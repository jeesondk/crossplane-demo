# Crossplane Playground

Based on [this guide](https://docs.crossplane.io/latest/getting-started/provider-azure/), this repo demo's how to create connect to azure and create a azure resource using crossplane.

## Vnet & RG added

### ResourceGroup

```yaml
apiVersion: azure.upbound.io/v1beta1
kind: ResourceGroup
metadata:
  name: docs
spec:
  forProvider:
    location: North Europe
```

```bash
kubectl create -f .\rg-example.yml
```


![image](https://github.com/jeesondk/crossplane-demo/assets/39995834/45f16efa-bb6f-40a9-9caa-3bf8573b85ee)

```bash
kubectl get resourcegroup
NAME   SYNCED   READY   EXTERNAL-NAME   AGE
docs   True     True    docs            4m23s
```


### VNet

```yaml
apiVersion: network.azure.upbound.io/v1beta1
kind: VirtualNetwork
metadata:
  name: crossplane-quickstart-network
spec:
  forProvider:
    addressSpace:
      - 10.0.0.0/16
    location: "North Europe"
    resourceGroupName: docs
```

```bash
kubectl create -f .\vnet-example.yml
```

![image](https://github.com/jeesondk/crossplane-demo/assets/39995834/50561773-e593-4689-9f4b-489c3a8bb026)

```bash
kubectl get virtualnetwork.network
NAME                            READY   SYNCED   EXTERNAL-NAME                   AGE
crossplane-quickstart-network   True    True     crossplane-quickstart-network   2m45s
```


## Vnet & RG removed

### Removing VNet
```bash
kubectl delete virtualnetwork.network crossplane-quickstart-network
    virtualnetwork.network.azure.upbound.io "crossplane-quickstart-network" deleted
```

```bash
kubectl get virtualnetwork.network
    No resources found
```
