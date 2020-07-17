# .NET AWS Lambda with SAM - Auto Scaling Manager

This project demonstrates the integration with **AWS Auto Scaling** service and **.NET Core**, using **SAM** for building and test.

This sample contains source code and supporting files for a **serverless application** that you can deploy with the **SAM CLI**. 

- src - Multilayer .NET Core project for the application's Lambda function.
- test - Unit tests for the application code with XUnit. 
- [`template.yaml`](./template.yaml) - A template that defines the application's AWS resources.

## How its works

Using a JSON as input is possible to **suspend** or **resume** processes from a specific auto scaling by _tag name_.

Use case: SUSPEND the _Terminate_ and _Launch_ processes before the initial stage from Blue-Green Deploy as a Code Pipeline stage and then, RESUME in the final stage.

JSON sample for using at AWS or this project.

```json5
[
    {
        "Tag": "your-tag-name-here",
        "Suspend": true
    }
]
```

## Function Project

This starter project consists of:
* [`Dotnet.AWSLambda.AutoScaling.Application.Function.cs`](./src/Dotnet.AWSLambda.AutoScaling.Application/Function.cs) - class file containing a class with a single function handler method;
* [`aws-lambda-tools-defaults.json`](./aws-lambda-tools-defaults.json) - default argument settings for use with **Rider** or **Visual Studio** and command line deployment tools for AWS.

## About SAM

The **Serverless Application Model Command Line Interface** (SAM CLI) is an extension of the AWS CLI that adds functionality for building and testing Lambda applications. It uses **Docker** to run your functions in an Amazon Linux environment that matches Lambda. It can also emulate your application's build environment and API.

To use the SAM CLI, you need the following tools.

* SAM CLI - [Install the SAM CLI](https://docs.aws.amazon.com/serverless-application-model/latest/developerguide/serverless-sam-cli-install.html)
* .NET Core - [Install .NET Core](https://www.microsoft.com/net/download)
* Docker - [Install Docker community edition](https://hub.docker.com/search/?type=edition&offering=community)

## Amazon.Lambda.Tools:

Install Amazon.Lambda.Tools Global Tools if not already installed.

```bash
dotnet tool install -g Amazon.Lambda.Tools
```

If already installed check if new version is available.

```bash
dotnet tool update -g Amazon.Lambda.Tools
```

## Use the SAM CLI to build and test locally

Build application with the `sam build` command.

```bash
sam build
```

The SAM CLI installs dependencies defined in `./src/Dotnet.AWSLambda.AutoScaling.Application/Dotnet.AWSLambda.AutoScaling.Application.csproj`, creates a deployment package, and saves it in the `.aws-sam/build` folder.

Test a single function by invoking it directly with a test **event**. An event is a JSON document that represents the input that the function receives from the event source. Test events are included in the [`event`](./event.json) file in this project.

Run functions locally and invoke them with the `sam local invoke` command.

```bash
sam local invoke -e event.json
```

The SAM CLI can also emulate the application's as API. Use the `sam local start-api` to run the API locally on port 3000.

```bash
sam local start-api
```

Then, is possible to request using **CURL** or **REST Client**:

```bash
curl --header "Content-Type: application/json" -X POST -d "{ 'Scalings': [ { 'Tag': 'your-tag-name-here', 'Suspend': false }, { 'Tag': 'your-tag-name-h', 'Suspend': true } ] }" http://127.0.0.1:3000/api
```

```http request
POST http://127.0.0.1:3000/api/
content-type: application/json

[
  {
        "Tag": "your-tag-name-here",
        "Suspend": false
    },
    {
        "Tag": "your-tag-name-here",
        "Suspend": true
    }
]
```

The SAM CLI reads the application template to determine the API's routes and the functions that they invoke.

```yaml
  Events:
    AutoScalingManager:
      Type: Api
      Properties:
        Path: '/api'
        Method: post
```

## Unit tests

Tests are defined in the `test` folder in this project.

```bash
dotnet test
```

## Credentials 

You can set credentials in the AWS credentials file on your local system. This file must be located in one of the following locations:

* `~/.aws/credentials` on Linux or macOS

* `C:\Users\USERNAME\.aws\credentials` on Windows

This file should contain lines in the following format:

```
[default]
aws_access_key_id = your_access_key_id
aws_secret_access_key = your_secret_access_key
```

* Environment variables – You can set the `AWS_ACCESS_KEY_ID` and `AWS_SECRET_ACCESS_KEY` environment variables.

To set these variables on Linux or macOS, use the export command: 

```
export AWS_ACCESS_KEY_ID=your_access_key_id
export AWS_SECRET_ACCESS_KEY=your_secret_access_key
```

To set these variables on Windows, use the set command: 

```
set AWS_ACCESS_KEY_ID=your_access_key_id
set AWS_SECRET_ACCESS_KEY=your_secret_access_key
```