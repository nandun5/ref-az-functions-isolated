# Contributing
> We use our wonderful Code of Conduct when contributing. This same template is used by over 350K open source projects.

Welcome! And thank you for your interest in contributing to this project. There are many ways in which you can contribute, beyond writing code. Here's a hi-level overview of how you can get involved:

- Ask questions and problems on MS Teams.
- Submit bugs and feature requests, and help us verify as they are checked in.
- Upvote popular feature requests.
- Review source code changes.
- Review the documentation and make pull requests for anything rom typos to new content.

## Contributing to Source Code
If you are interested in writing code to fix issues, here's a hi-level overview of how you can clone the repos and get started.

### Prerequisites
In order to download necessary tools, clone the repo, and install dependencies via `dotnet restore`, you need internet access.

You'll need following tools:
- Git
- Visual Studio 2022
  > Or VS Code with Recommended Extensions
- Azure Functions Core Tools v4
- Access to reference

## Build and Run
If you wanna understand how the package works, or debug an issue, you'll want to get the source, build it, and run it locally.

### Getting the source code.
First, create a feature branch so that you can make a Pull Request. Then clone the repo locally:

```bash
#!/bin/bash
git clone https://github.com/nandun5/ref-az-functions-isolated.git
code ec-WorkerFunction
```

### Build and run from source
First, you wanna ensure you are connected to the internet so that the http endpoint URIs can be accessed.

With VS Code:
- Press `F1`, then run task `build`
- Press `F5` to launch the function app.

Then, sprin up your favorite REST Client to visit http://localhost:7071/health.

### Writing unit test specifications

Azure Functions Worker not yet mature when it comes to writing unit tests against the Functions, especially, for HTTP Triggers. You can follow up the conversation [@azure/azure-functions-dotnet-worker/issue/281](https://github.com/Azure/azure-functions-dotnet-worker/issues/281) on GitHub.

To test changes, you will need to use the MS Tests.

With VS Code:
- Press `F1`, then run task `build`
- Press `F1`, then run task `test`

## Pull Requests
To enable us to quickly review and accept your pull requests, always create one pull requests per backlog item, link the tasks in the pull request. Never merge multiple into one unless they all share the same root cause. Never link the backlog item to the pull request.

Be sure to follow the community guidelines and keep the source code changes as small as possible. Avoid any pure document formatting changes to code that has not been modified otherwise.

To avoid multiple pull requests resolving the same root cause or backlog item, let others know you are working on it by saying so in the sprint backlog.

## Publishing
At present, the function apps are published via Azure YAML Pipelines to: reference resource group. To get familiar, checkout `.azure` folder for the pipeline scripts.

## Discussion Etiquette
In order to keep the conversations clear and transparent, please limit discussion to English and keep things on topic with the scope. Be considerate to others and try to be courteous and professional at all times.

## Thank You
Thank you for taking time to contribute.