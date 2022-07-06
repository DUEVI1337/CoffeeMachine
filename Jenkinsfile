pipeline {
  agent { label 'duevi' }
  options {
    timestamps()
    timeout(time: 1, unit: 'HOURS')
  }
  environment {
    PROJECT_NAME = 'CoffeeMachine_Duvanov'
    NAME = 'registry.tomskasu.ru/trainee/back-intern/coffeemachine_duvanov'
    TAG = 'latest'
  }
  stages {
    stage('Build') {
      environment {
          ASPNETCORE_ENVIRONMENT = 'Production'
          DOTNET_CLI_TELEMETRY_OPTOUT = 'true'
          DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
      }
	  steps {
        bat(script: 'dotnet restore CoffeeMachine.sln',
            label: 'Restore')
        bat(script: 'dotnet build CoffeeMachine.sln --configuration Release --no-restore',
            label: 'build app')
        bat(script: 'dotnet test tests/CoffeeMachine.UnitTests/CoffeeMachine.UnitTests.csproj',
            label: 'unit tests')
        bat(script: 'dotnet test tests/CoffeeMachine.IntegrationTests/CoffeeMachine.IntegrationTests.csproj',
            label: 'integration tests')
        bat(script: 'dotnet publish CoffeeMachine.sln --configuration Release --output app',
            label: 'publish app')
      }
    }
    stage('Docker image create and push') {
      when {
        not {
          branch 'main'
        }
      }
      steps {
        script {
          def BackImage = docker.build("${env:NAME}:${env:TAG}")
          BackImage.push()
 
          BackImage.push("${env:BUILD_NUMBER}")
        }
      }
    }
  }
 
  post {
    always {
      cleanWs(cleanWhenNotBuilt: false,
        deleteDirs: true,
        disableDeferredWipeout: true,
        notFailBuild: true,
        patterns: [[pattern: '.gitignore', type: 'INCLUDE'],
          [pattern: '.propsfile', type: 'EXCLUDE'],
          [pattern: 'bin', type: 'INCLUDE'],
          [pattern: 'app', type: 'INCLUDE']])
    }
  }
}
