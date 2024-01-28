pipeline{
    
    agent any
    environment {
        registry = "excellerentsolutions/eppbe"
        registryCredential = 'dockerhubID'
        eppbeImage = ''
        
        
    }
    
    stages
    { 
       // stage('Git checkout')
       // {
       //     steps{
       //       git credentialsId: 'jenkins-bitbucket-omeseret', url: 'https://bitbucket.org/Excellerent_Solutions/excellerent-epp-be'
       // 
       //     }
       // }
        stage('Dotnet build for develop')
        {
            when {
                branch 'develop'
            }
            agent{
             docker
                {
                 image 'mcr.microsoft.com/dotnet/sdk:5.0'
                  args '-u root:root'
                }
            }
            steps{
              sh 'dotnet build' 
            }
        }
        stage('Dotnet build for release')
        {
            when {
                branch 'release'
            }
            agent{
             docker
                {
                 image 'mcr.microsoft.com/dotnet/sdk:5.0'
                  args '-u root:root'
                }
            }
            steps{
              sh 'git branch -D release || true'
              sh 'git checkout -b release origin/release'
              sh 'git branch'
              sh 'dotnet build' 
            }
        }
        stage('Dotnet build for master')
        {
            when {
                branch 'master'
            }
            agent{
             docker
                {
                 image 'mcr.microsoft.com/dotnet/sdk:5.0'
                  args '-u root:root'
                }
            }
            steps{
              sh 'git branch -D master || true'
              sh 'git checkout -b master origin/master'
              sh 'git branch'
              sh 'dotnet build' 
            }
        }
        stage('Dotnet test for release')
        {
            when {
                branch 'release'
            }        
            agent{
             docker
                {
                 image 'mcr.microsoft.com/dotnet/sdk:5.0'
                  args '-u root:root'
                }
            }
            steps{
              sh 'git branch'
              sh 'git checkout release'
              sh 'dotnet test'  
            }
        }
        stage('Dotnet test for master')
        {
            when {
                branch 'master'
            }            
            agent{
             docker
                {
                 image 'mcr.microsoft.com/dotnet/sdk:5.0'
                  args '-u root:root'
                }
            }
            steps{
              sh 'git branch'
              sh 'git checkout master'
              sh 'dotnet test'  
            }
        }
        stage('Deploy to Staging')
        {
            when {
                branch 'release'
            }
            steps{
                script 
                {
                
                 
                    sshagent(credentials : ['dev']) {
                        eppbeImage = docker.build registry + (":release-latest")
                        docker.withRegistry( '', 'dockerhubID' )
                            {
                             eppbeImage.push()
                                 
                  sh "rsync -rv --delete -e 'ssh' ./docker-compose.release.yml ubuntu@epp-be.excellerentsolutions.com:/home/ubuntu/deployment"  
                  
                  sh "ssh -o StrictHostKeyChecking=no  ubuntu@epp-be.excellerentsolutions.com sudo docker-compose -f /home/ubuntu/deployment/docker-compose.release.yml down"
                  sh "ssh -o StrictHostKeyChecking=no  ubuntu@epp-be.excellerentsolutions.com sudo docker system prune -af"
                  sh "ssh -o StrictHostKeyChecking=no  ubuntu@epp-be.excellerentsolutions.com sudo docker-compose -f /home/ubuntu/deployment/docker-compose.release.yml up -d "}
                            }
                      
                    }
              //clean the workspace after deployment
                cleanWs deleteDirs: true, notFailBuild: true
            }     
                 
            
        }
    
        stage('Deploy to Production')
        {
            when {
                branch 'master'
            }
            steps{
                script 
                {
                
                 
                    sshagent(credentials : ['epp-prod-ssh']) {
                        eppbeImage = docker.build registry + (":latest")
                        docker.withRegistry( '', 'dockerhubID' )
                            {
                             eppbeImage.push()
                                 
                  sh "rsync -rv --delete -e 'ssh' ./docker-compose.yml ubuntu@api.epp-excellerentsolutions.com:/home/ubuntu/deployment"  
                  
                  sh "ssh -o StrictHostKeyChecking=no  ubuntu@api.epp-excellerentsolutions.com sudo docker-compose -f /home/ubuntu/deployment/docker-compose.yml down"
                  sh "ssh -o StrictHostKeyChecking=no  ubuntu@api.epp-excellerentsolutions.com sudo docker system prune -af"
                  sh "ssh -o StrictHostKeyChecking=no  ubuntu@api.epp-excellerentsolutions.com sudo docker-compose -f /home/ubuntu/deployment/docker-compose.yml up -d "}
                            }
                      
                    }
              //clean the workspace after deployment
                cleanWs deleteDirs: true, notFailBuild: true
            }     
                 
            
        }
    }
}
