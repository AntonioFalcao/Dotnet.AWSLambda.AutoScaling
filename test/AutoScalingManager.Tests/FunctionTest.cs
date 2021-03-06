using Dotnet.AWSLambda.AutoScaling.Application;
using Newtonsoft.Json.Linq;
using Xunit;

namespace AutoScalingManager.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void Should_Handle_With_HttpRequest_As_Input()
        {
            const string jString =
                @"{
                    'httpMethod': 'POST',
                    'body': '{ \'Scalings\': [ { \'Tag\': \'tag-name-here\', \'Suspend\': false }, { \'Tag\': \'tag-name-here\', \'Suspend\': true } ] }',
                    'resource': '/api',
                    'requestContext': {
                        'resourceId': '123456',
                        'apiId': '1234567890',
                        'resourcePath': '/api',
                        'httpMethod': 'POST',
                        'requestId': 'c6af9ac6-7b61-11e6-9a41-93e8deadbeef',
                        'accountId': '123456789012',
                        'stage': 'Prod',
                        'identity': {
                            'apiKey': null,
                            'userArn': null,
                            'cognitoAuthenticationType': null,
                            'caller': null,
                            'userAgent': 'Custom User Agent String',
                            'user': null,
                            'cognitoIdentityPoolId': null,
                            'cognitoAuthenticationProvider': null,
                            'sourceIp': '127.0.0.1',
                            'accountId': null
                        },
                        'extendedRequestId': null,
                        'path': '/api'
                    },
                    'queryStringParameters': null,
                    'multiValueQueryStringParameters': null,
                    'headers': {
                        'Host': '127.0.0.1:3000',
                        'User-Agent': 'curl/7.66.0',
                        'Accept': '*/*',
                        'Content-Type': 'application/json',
                        'Content-Length': '116',
                        'X-Forwarded-Proto': 'http',
                        'X-Forwarded-Port': '3000'
                    },
                    'multiValueHeaders': {
                        'Host': [
                            '127.0.0.1:3000'
                        ],
                        'User-Agent': [
                            'curl/7.66.0'
                        ],
                        'Accept': [
                            '*/*'
                        ],
                        'Content-Type': [
                            'application/json'
                        ],
                        'Content-Length': [
                            '116'
                        ],
                        'X-Forwarded-Proto': [
                            'http'
                        ],
                        'X-Forwarded-Port': [
                            '3000'
                        ]
                    },
                    'pathParameters': null,
                    'stageVariables': null,
                    'path': '/api',
                    'isBase64Encoded': false
                }";

            var jObject = JObject.Parse(jString);
            var functionTask = Function.HandleAsync(jObject);
            functionTask.GetAwaiter().GetResult();
        }

        [Fact]
        public void Should_Handle_With_Simple_Json_As_Input()
        {
            const string jString =
                @"{
                    'Scalings': [
                        {
                            'Tag': 'tag-name-here',
                            'Suspend': false
                        },
                        {
                            'Tag': 'tag-name-here',
                            'Suspend': true
                        }
                    ]
                }";

            var jObject = JObject.Parse(jString);
            var functionTask = Function.HandleAsync(jObject);
            functionTask.GetAwaiter().GetResult();
        }
        
        [Fact]
        public void Should_Handle_With_Simple_Json_With_Processes_As_Input()
        {
            const string jString =
                @"{
                    'Scalings': [
                        {
                            'Tag': 'tag-name-here',
                            'Suspend': false,
                            'Processes': [
                                'Terminate',
                                'Launch'
                            ]
                        }
                    ]
                }";

            var jObject = JObject.Parse(jString);
            var functionTask = Function.HandleAsync(jObject);
            functionTask.GetAwaiter().GetResult();
        }
    }
}