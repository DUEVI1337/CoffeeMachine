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
      agent { dockerfile true }
      environment {
          ASPNETCORE_ENVIRONMENT = 'Production'
          DOTNET_CLI_TELEMETRY_OPTOUT = 'true'
          DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
      }
	  steps {
        sh(script: 'dotnet restore CoffeeMachine.sln',
            label: 'Restore')
        sh(script: 'dotnet build CoffeeMachine.sln --configuration Release --no-restore',
            label: 'build app')
        sh(script: 'dotnet publish CoffeeMachine.sln--configuration Release --output app',
            label: 'publish app')
         sh(script: 'chmod -R 777 app/',
          label: 'changed rules on app directory')
      }
    }
 
    stage('Docker image create and push') {
      when {
        branch 'HRI-14'
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
