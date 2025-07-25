using GraphQL;
using GraphQL.Client.Abstractions;
using MyDNA.GraphQLClients.BlazorWAS.ThuNTN.Models;

using LoginResponse = MyDNA.GraphQLClients.BlazorWAS.ThuNTN.Models.LoginResponse;


namespace MyDNA.GraphQLClients.BlazorWAS.ThuNTN.GraphQLClients
{
    public class GraphQLConsumer
    {
        private readonly GraphQLClientProvider _graphQLClient;

        public GraphQLConsumer(GraphQLClientProvider graphQLClient) => _graphQLClient = graphQLClient;

        public async Task<List<TestResultsThuNtn>> getTestResultsThuNtn()
        {
            try
            {
                var request = new GraphQLRequest
                {
                    Query = @"
                query {
                    allTestResults {
                        testResultThuNtnid
                        orderId
                        resultVersion
                        resultSummary
                        resultDetail
                        resultFileUrl
                        issuedBy
                        completedAt
                        createdAt
                        resultStatus
                    }
                }"
                };
                var client = await _graphQLClient.GetClientAsync();

                var response = await client.SendQueryAsync<TestResultsThuNtnResponse>(request);
                return response?.Data?.allTestResults ?? new List<TestResultsThuNtn>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi gọi GraphQL: " + ex.Message);
                return new List<TestResultsThuNtn>();
            }
        }


        public async Task<TestResultsThuNtn> getTestResultById(int id)
        {
            try
            {
                var query = $@"query TestResultById($id: Int!) {{
                        testResultById(id: $id) {{
                            testResultThuNtnid
                            orderId
                            resultVersion
                            resultSummary
                            resultDetail
                            resultFileUrl
                            issuedBy
                            completedAt
                            createdAt
                            resultStatus
                        }}
                    }}";

                var client = await _graphQLClient.GetClientAsync();

                var response = await client.SendQueryAsync<TestResultsThuNtnDetailResponse>(query, new { id });

                return response?.Data.testResultById;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public async Task<int> CreateTestResultThuNtn(TestResultsThuNtn testResult)
        {
            try
            {
                var request = new GraphQLRequest
                {
                    Query = @"
        mutation CreateTestResult($input: TestResultsThuNtnInput!) {
            createTestResult(testResultsThuNtn: $input)
        }",
                    Variables = new
                    {
                        input = new
                        {
                            testResultThuNtnid = testResult.TestResultThuNtnid, // <-- để mặc định
                            orderId = testResult.OrderId,
                            resultVersion = testResult.ResultVersion,
                            resultSummary = testResult.ResultSummary,
                            resultDetail = testResult.ResultDetail,
                            resultFileUrl = testResult.ResultFileUrl,
                            issuedBy = testResult.IssuedBy,
                            completedAt = testResult.CompletedAt?.ToUniversalTime(), // <-- để mặc định
                            createdAt = testResult.CreatedAt?.ToUniversalTime(),
                            resultStatus = testResult.ResultStatus
                        }
                    }
                };

                var client = await _graphQLClient.GetClientAsync();

                var response = await client.SendMutationAsync<CreateTestResultResponse>(request);

                return response?.Data?.createTestResult ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi gọi mutation CreateTestResult: " + ex.Message);
                return 0;
            }
        }


        public async Task<int> UpdateTestResultThuNtn(TestResultsThuNtn testResult)
        {
            try
            {
                var request = new GraphQLRequest
                {
                    Query = @"
mutation UpdateTestResult($input: TestResultsThuNtnInput!) {
    updateTestResult(testResultsThuNtn: $input)
}",
                    Variables = new
                    {
                        input = new
                        {
                            testResultThuNtnid = testResult.TestResultThuNtnid, // bắt buộc phải có ID khi update
                            orderId = testResult.OrderId,
                            resultVersion = testResult.ResultVersion,
                            resultSummary = testResult.ResultSummary,
                            resultDetail = testResult.ResultDetail,
                            resultFileUrl = testResult.ResultFileUrl,
                            issuedBy = testResult.IssuedBy,
                            completedAt = testResult.CompletedAt?.ToUniversalTime(),
                            createdAt = testResult.CreatedAt?.ToUniversalTime(),
                            resultStatus = testResult.ResultStatus
                        }
                    }
                };
                var client = await _graphQLClient.GetClientAsync();

                var response = await client.SendMutationAsync<UpdateTestResultResponse>(request);

                return response?.Data?.updateTestResult ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi gọi mutation UpdateTestResult: " + ex.Message);
                return 0;
            }
        }

        public async Task<bool> deleteTestResult(int id)
        {
            try
            {
                var request = new GraphQLRequest
                {
                    Query = @"
                mutation DeleteTestResult($id: Int!) {
                    deleteTestResult(id: $id)
                }
            ",
                    Variables = new { id }
                };
                var client = await _graphQLClient.GetClientAsync();
                var response = await client.SendMutationAsync<deleteTestResultResponse>(request);
                return response.Data.deleteTestResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa kết quả xét nghiệm: " + ex.Message);
                return false;
            }
        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            var request = new GraphQLRequest
            {
                Query = @"
mutation Login($username: String!, $password: String!) {
  login(username: $username, password: $password) {
    token
    fullName
    role
  }
}",
                Variables = new
                {
                    username = email,
                    password = password
                }
            };
            var client = await _graphQLClient.GetClientAsync();

            var response = await client.SendMutationAsync<LoginResponseWithUserAccount>(request);

            var loginData = response.Data?.login;

           

            return loginData;
        }
    }


}


