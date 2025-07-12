using GraphQL;
using GraphQL.Client.Abstractions;
using MyDNA.GraphQLClients.BlazorWAS.ThuNTN.Models;


namespace MyDNA.GraphQLClients.BlazorWAS.ThuNTN.GraphQLClients
{
    public class GraphQLConsumer
    {
        private readonly IGraphQLClient _graphQLClient;

        public GraphQLConsumer(IGraphQLClient graphQLClient) => _graphQLClient = graphQLClient;

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

                var response = await _graphQLClient.SendQueryAsync<TestResultsThuNtnResponse>(request);
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
                var query = $@"query TestResultById($id: Int!) {{    testResultThuNtn(testResultThuNtnid: $id) {{        testResultThuNtnid        orderId        resultVersion        resultSummary       resultDetail        resultFileUrl        issuedBy        completedAt        createdAt        resultStatus    }} }}";
                var response = await _graphQLClient.SendQueryAsync<TestResultsThuNtnResponse>(query, new { id });

                return response?.Data?.allTestResults?.FirstOrDefault();
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
                mutation CreateTestResultThuNtn($input: TestResultsThuNtnInput!) {
                    createTestResultThuNtn(testResult: $input)
                }",
                    Variables = new
                    {
                        input = new
                        {
                            orderId = testResult.OrderId,
                            resultVersion = testResult.ResultVersion,
                            resultSummary = testResult.ResultSummary,
                            resultDetail = testResult.ResultDetail,
                            resultFileUrl = testResult.ResultFileUrl,
                            issuedBy = testResult.IssuedBy,
                            completedAt = testResult.CompletedAt,
                            createdAt = testResult.CreatedAt,
                            resultStatus = testResult.ResultStatus
                        }
                    }
                };

                var response = await _graphQLClient.SendMutationAsync<CreateTestResultResponse>(request);

                return response?.Data?.createTestResultThuNtn ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi gọi mutation CreateTestResult: " + ex.Message);
                return 0;
            }
        }



    }
}
