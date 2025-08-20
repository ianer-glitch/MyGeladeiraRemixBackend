# My Geladeira Remix

The solution addresses the problem: *“What can I cook with what I already have?”*

![imagem architecture ](doc/architecture.jpg)

## Services

- **Fridge Service**: Responsible for managing the user's food inventory. It handles communication with the File Storage service and OpenAI.

- **Statistics Service**: Receives data about food consumption from the Fridge Service to generate statistics and insights on consumption habits.

- **Identity Service**: Serves as a user aggregator, managing user data stored in the User Service and Plan Service via gRPC.

- **Plan Service**: Not yet implemented, but intended to handle plans and payments.

- **Service Bus**: A RabbitMQ message broker for sending information between microservices.

- **File Storage**: A MinIO service which stores food icons.

- **Front End Service**: An Angular project, which is deployed together with the back-end in a single Kubernetes cluster through an NGINX load balancer running as a reverse proxy.