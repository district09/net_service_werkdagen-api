#!/usr/bin/env groovy

@Library("jenkins-shared-pipelines") _

d09Project  jenkinsCiAgent: "netcore31",
            ciSkipChangelog: false,
            cdSemanticRelease: true,
            cdServices: ["werkdagen-api-v1"],
			openshiftDeployNamespace: "opendata",
            gitOpsPath: "opendata-services/werkdagen-api-v1",
            dotnetFolder: "src/",
            dotnetSlnFile: "District09.Servicefactory.Werkdagen.sln", 
            dotnetTestFolder: "District09.Servicefactory.Werkdagen.Tests",
            dotnetTestFile: "District09.Servicefactory.Werkdagen.Tests.csproj"
