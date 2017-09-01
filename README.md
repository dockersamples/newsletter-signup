# Newsletter Sign Up - .NET 3.5

This is a branch of the [Newsletter Sign Up app](https://github.com/dockersamples/newsletter-signup) which uses a .NET 3.5 cut of the code. 

> This is the sample application used in the video series [Modernizing .NET Apps for IT Pros](https://dockr.ly/mta-itpro).

A .NET Framework app using Docker containers on Windows. The app lets users sign up to a fictional newsletter:

![SignUp homepage](img/signup-homepage.png)

## Architecture

This is a distributed application, running across multiple containers (the final version is defined in [docker-stack-v2.yml](app/part-4/docker-stack-v2.yml)):

- `db` - [SQL Server Express](https://store.docker.com/images/mssql-server-windows-express), used to store prospect details
- `app` - ASP.NET 3.5 WebForms application, front end for prospects to sign up
- `prometheus` - [Prometheus](https://prometheus.io) monitoring, used to display Windows Performance Counter metrics from the web container

## Part 1

The first video in the series explains the concepts of Modernizing Traditional Apps (**MTA**) and the goals of the series:

* the problems of traditional apps, which are built to run on servers and have unique deployment, administration and upgrade processes
* the benefits of moving traditional apps to the Docker platform - efficiency, portability and security
* how you can migrate .NET apps from Windows Server to Docker with no code changes
* how the Docker version of the app is portable, and runs in the same way in the datacenter and in the cloud
* how all applications are consistent in Docker - they are packaged, distributed, deployed and managed using the same simple tools

> Watch [Modernizing .NET Apps for IT Pros - Part 1](https://www.youtube.com/watch?v=gaJ9PzihAYw&index=1&list=PLkA60AVN3hh88hW4dJXMFIGmTQ4iDBVBp)

## Part 2

Uses the [Image2Docker](https://github.com/docker/communitytools-image2docker-win) tool to extract the ASP.NET 3.5 application from a Windows Server 2003 VM to Docker. The tool extracts all the web application binaries, content and configuration from the VM (stored in this repo under [SignUp.Web](docker/web/SignUp.Web)). It also generates a [Dockerfile](docker/web/Dockerfile.v1) to package the app as a Docker image.

The video shows how to add features by iterating on the generated Dockerfile. [Version 2](docker/web/Dockerfile.v2) changes the startup command and relays the application log entries to Docker, so you can see the .NET logs using `docker container logs`.

> Watch [Modernizing .NET Apps for IT Pros - Part 2](https://www.youtube.com/watch?v=7rNTYslgJdQ&index=2&list=PLkA60AVN3hh88hW4dJXMFIGmTQ4iDBVBp)

## Part 3

Demonstrates the update workflow for .NET apps running in Docker Windows containers. In the video you'll see version 2 of the app deployed to a Windows Server 2016 VM running in Azure. That's a .NET 3.5 app moved from an on-premises Windows Server 2003 VM to the cloud in under 10 minutes!

[Version 3](docker/web/Dockerfile.v3) of the Dockerfile uses a newer version of the Windows Server Docker image, which has hotfixes and security patches built in. In Docker you deploy a Windows update to your app by building a new image from the latest Windows image, and replacing your running container. 

> Watch [Modernizing .NET Apps for IT Pros - Part 3](https://www.youtube.com/watch?v=G6txVNk-Q-s&index=3&list=PLkA60AVN3hh88hW4dJXMFIGmTQ4iDBVBp)

## Part 4

Deploys the app to a highly-available staging environment running on Azure and managed with [Docker Cloud](https://cloud.docker.com). It's still the same ASP.NET 3.5 app with no code changes, but now it's running in a scalable environment where the Docker platform can execute zero-downtime updates.

[Version 4](docker/web/Dockerfile.v4) of the Dockerfile also exposes the Windows Performance Counters from the container, and the video shows how you can run Prometheus in a Docker Windows container. The Prometheus container reads performance counter values from the web application container and gives you a consistent approach to monitoring in every environment.

> Watch [Modernizing .NET Apps for IT Pros - Part 4](https://www.youtube.com/watch?v=lPjO6My2NLE&index=4&list=PLkA60AVN3hh88hW4dJXMFIGmTQ4iDBVBp)

## Part 5

Shows you how Docker applications look in production, using [Docker Enterprise Edition](https://www.docker.com/enterprise-edition) - which you can run in the cloud or in the datacenter. The video uses [Universal Control Plane](https://docs.docker.com/datacenter/ucp/2.2/guides/) and [Docker Trusted Registry](https://docs.docker.com/datacenter/dtr/2.3/guides/) as the production-grade Containers-as-a-Service (**CaaS**) platform for deploying, updating and managing the app.

[Version 5](docker/web/Dockerfile.v5) of the application image uses [Docker secrets](https://docs.docker.com/engine/swarm/secrets/) to securely store the connection string for the application database. Docker containers can integrate with services running outside of Docker, andf the video shows the web application running in a Docker Windows container, storing data in a SQL Azure database.

> Watch [Modernizing .NET Apps for IT Pros - Part 5](https://www.youtube.com/watch?v=f288C_Vqkx4&index=5&list=PLkA60AVN3hh88hW4dJXMFIGmTQ4iDBVBp)