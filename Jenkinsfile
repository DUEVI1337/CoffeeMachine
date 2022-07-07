pipeline {
  agent { label 'tap-agents' }
  options {
    timestamps()
    timeout(time: 1, unit: 'HOURS')
    gitLabConnection('tomskasu')
    gitlabBuilds(builds: ['Build', 'Docker image create and push'])
  }
  environment {
    PROJECT_NAME = 'CoffeeMachine_Duvanov'
    NAME = 'registry.tomskasu.ru/trainee/back-intern/coffeemachine_duvanov'
    TAG = 'latest'
  }
  stages {
    stage('Build') {
      agent {
        docker {
          image 'registry.tomskasu.ru/devops/dockerify/dotnetsdk:5.0-focal'
          args "-v ${PWD}:/usr/src/app -w /usr/src/app -u root --privileged -e PATH='/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/root/.dotnet/tools'"
          reuseNode true
          label 'build-image'
        }
      }      
      environment {
          ASPNETCORE_ENVIRONMENT = 'Production'
          DOTNET_CLI_TELEMETRY_OPTOUT = 'true'
          DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
      }
	    steps {
	      gitlabCommitStatus("Build") {
			  withSonarQubeEnv('sonar.tomskasu.ru') {
				  sh(script: 'dotnet restore CoffeeMachine.sln',
					  label: 'Restore')
				  sh(script: 'dotnet build CoffeeMachine.sln --configuration Release --no-restore',
					  label: 'build app')
				  sh(script: 'dotnet test tests/CoffeeMachine.UnitTests/CoffeeMachine.UnitTests.csproj --logger "trx;LogFileName=unit_tests.xml"',
					  label: 'unit tests')  
				  sh(script: 'dotnet test tests/CoffeeMachine.IntegrationTests/CoffeeMachine.IntegrationTests.csproj --logger "trx;LogFileName=integration_tests.xml"',
					  label: 'integration tests')  
			  }	        
			  sh(script: 'dotnet publish CoffeeMachine.sln --configuration Release --output app',
				label: 'publish app')      
			  sh(script: 'chmod -R 777 app/',
				label: 'changed rules on app directory')
	      }
      }
    }
    stage('Docker image create and push') {
      when {
        branch 'main'
      }
      steps {
        gitlabCommitStatus("Docker image create and push") {
			sh(script: 'docker login registry.tomskasu.ru -u DuvanovEV -p glpat-xrL3z-xNC-o4VTvajyuJ')
			script {
			  def BackImage = docker.build("${env:NAME}:${env:TAG}")
			  BackImage.push()
	 
			  BackImage.push("${env:BUILD_NUMBER}")
			}
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
