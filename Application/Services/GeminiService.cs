using Application.DTOs.AIDtos;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Application.Services
{
    public class GeminiService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        
        private const string ModelId = "gemini-3.1-flash-lite";
        private const string BaseUrl = "https://generativelanguage.googleapis.com/v1beta/models";

        public GeminiService(IConfiguration config)
        {
            _httpClient = new HttpClient();
            _apiKey = config["Gemini:ApiKey"];
        }

        private async Task<string> CallGeminiAsync(string prompt)
        {
            var url = $"{BaseUrl}/{ModelId}:generateContent?key={_apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                
                throw new Exception($"Gemini API Error {response.StatusCode}: {responseBody}");
            }

            using var doc = JsonDocument.Parse(responseBody);

            
            return doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString() ?? "";
        }

        public async Task<CoverLetterResponseDto> GenerateCoverLetterAsync(CoverLetterRequestDto dto)
        {
            
            var prompt = $@"
        Role: You are a professional career coach and expert technical writer.
        Task: Write a high-converting, professional cover letter for a {dto.JobTitle} position.
        
        Input Data:
        - Job Description: {dto.JobDescription}
        - Candidate Bio: {dto.CandidateBio}
        - Candidate Skills: {dto.CandidateSkills}

        Constraints:
        1. Tone: Professional, confident, and direct. 
        2. Format: 3 paragraphs max. Do NOT include placeholders like [Date] or [Address].
        3. Opening: Start with a strong hook about why the candidate is a perfect fit.
        4. Body: Focus on how the candidate's specific skills (like .NET or React) solve the company's problems mentioned in the JD.
        5. Closing: End with a professional call to action.
        6. Language: Use natural, human-like flow. Avoid clichés like 'I am writing to express my interest'.
    ";

            try
            {
                var result = await CallGeminiAsync(prompt);

              
                var cleanedResult = result.Trim();

                return new CoverLetterResponseDto { CoverLetter = cleanedResult };
            }
            catch (Exception ex)
            {
                return new CoverLetterResponseDto
                {
                    CoverLetter = $"Error generating letter: {ex.Message}"
                };
            }
        }

        public async Task<MatchScoreResponseDto> GenerateMatchScoreAsync(MatchScoreRequestDto dto)
        {
            
            var prompt = $@"You are an expert recruiter. Analyze the match between the job and candidate.
            Job: {dto.JobTitle} - {dto.JobDescription}
            Candidate: {dto.CandidateName}
            Cover Letter: {dto.CoverLetter ?? "Not provided"}

            Respond with ONLY this JSON format, no markdown, no extra text:
            {{ ""score"": 85, ""reason"": ""Brief explanation here"" }}";

            try
            {
                var result = await CallGeminiAsync(prompt);

             
                var cleanedResult = result.Replace("```json", "").Replace("```", "").Trim();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var parsed = JsonSerializer.Deserialize<MatchScoreResponseDto>(cleanedResult, options);

                return parsed ?? new MatchScoreResponseDto { Score = 0, Reason = "Could not parse AI response." };
            }
            catch (Exception ex)
            {
                return new MatchScoreResponseDto { Score = 0, Reason = $"Analysis failed: {ex.Message}" };
            }
        }
    }
}