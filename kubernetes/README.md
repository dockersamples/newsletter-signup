
# Running a Hybrid Windows/Linux Cluster with Docker and Kubernetes on ACS

> TODO. Tidy up the notes.


## 1. Pre-reqs

Install ACS Engine:

```
wget -o acs-engine https://github.com/Azure/acs-engine/releases/download/v0.12.3/acs-engine-v0.12.3-linux-amd64.tar.gz

tar xvf acs-engine-v0.12.3-linux-amd64.tar.gz
```

You will need an SSH key to connect to the Kube master, and an Azure Service Principal for ACS Engine to deploy resources:

```
ssh-keygen -t rsa -C k8s
```

```
# get subscription ID from az account list
az ad sp create-for-rbac --role Contributor --scopes="/subscriptions/[SUBSCRIPTION_ID]"
```

## 2. Update ACS Engine Defintion Doc

Replace tokens in `kube-1.9.json` with your DNS prefix and credentials:

- YOUR_DNS_PREFIX
- YOUR_ADMIN_PASSWORD
- YOUR_SSH_PUBLIC_KEY
- YOUR_SP_APP_ID
- YOUR_SP_PASSWORD

## 3. Generate ARM Template with ACS Engine

```
acs-engine generate kube-1.9.json
```

## 4. Create Resource Group and Deploy

```
az group create -n k8s -l westeurope

az group deployment create -g k8s --template-file azuredeploy.json --parameters azuredeploy.parameters.json
```

## 5. Connect to Kube Master and Deploy

Master FQDN shows in deployment output (in CLI and in portal). Connect with SSH, then deploy app manifests:

```
kubectl apply -f db.yaml

kubectl apply -f message-queue.yaml

kubectl apply -f web.yaml
```