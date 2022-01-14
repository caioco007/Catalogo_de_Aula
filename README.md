# Catalogo de Aula

O Catalogo_de_Aula disponibiliza uma API REST que permite o acesso aos modulos e aulas.

## Migrations

**referências:**
- https://docs.microsoft.com/pt-br/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
- https://docs.microsoft.com/pt-br/ef/core/cli/dotnet

**steps:**

1. antes de rodar os comandos de migrations, entrar no folder raiz da solução
    ```cmd
    cd Projeto_API
    ```

2. instalar cli do ef (entity framework)
    ```cmd
    dotnet tool install --global dotnet-ef
    ```

3. para verificar se ef está instalado na máquina
    ```cmd
    dotnet ef
    ```

4. para usar o ef em um projeto específico, você precisará adicionar uma package ao seu projeto
    ```cmd
    dotnet add Projeto_API package Microsoft.EntityFrameworkCore.Design
    ```

5. adicionando um migration ao projeto
  No projeto ja esta com o 'Migration' criado.
    ```cmd
    dotnet ef migrations add AddInitMigration --startup-project Projeto_API --project Projeto_API --context ModuloContext --output-dir Data/Migrations
    ```

6. aplicando migrations em banco de dados
    ```cmd
    dotnet ef database update --project Projeto_API --context ModuloContext
    ```




Recursos disponíveis para acesso via API:
* **Módulos**
* **Aulas**
* **Usuários**

## Métodos
Requisições para a API devem seguir os padrões:
| Método | Descrição |
|---|---|
| `GET` | Retorna informações de um ou mais registros. |
| `POST` | Utilizado para criar um novo registro. |
| `PUT` | Atualiza dados de um registro ou altera sua situação. |
| `DELETE` | Remove um registro do sistema. |


## Respostas

| Código | Descrição |
|---|---|
| `200` | Requisição executada com sucesso (success).|
| `400` | Erros de validação ou os campos informados não existem no sistema.|
| `401` | Dados de acesso inválidos.|
| `404` | Registro pesquisado não encontrado (Not found).|
| `405` | Método não implementado.|
| `410` | Registro pesquisado foi apagado do sistema e não esta mais disponível.|
| `422` | Dados informados estão fora do escopo definido para o campo.|
| `429` | Número máximo de requisições atingido. (*aguarde alguns segundos e tente novamente*)|

---

## Solicitando tokens de acesso [/api/v1/account/login]

### Utilizando o código de acesso [POST]
Utilizando o `code` enviado pelo servidor de autorização, envie um POST com seus dados para receber um `access_token`.
O `access_token` é válido por 2 horas. Utilize o refresh_token para solicitar um novo access_token, para não solicitar ao usuário suas credenciais (login e senha) novamente.

| Parâmetro | Descrição |
|---|---|
| `username` | Informar: O Username do seu login. |
| `password` | Informar: A senha do seu login. |

+ Request (application/json)

    + Body

            {   
                "username": "[usuário ja criado]",
                "password": "[password ja criado]"
            }

+ Response 200 (application/json)

    + Body

            {
                "user": {
                    "id": [id do usuário],
                    "username": [nome do usuário],
                    "password": "",
                    "role": [role do usuário]
                },
                "token": [access_token]
            }


### Utilizando refresh_token [POST]

Utilize o último `refresh_token` recebido para solicitar um novo `access_token`. Após utilizado o `refresh_token`, este será invalidado e um novo será fornecido em conjunto com o `access_token`.

#### Dados para envio no POST
|| Parâmetro | Descrição |
|---|---|
| `username` | Informar: O Username do seu login. |
| `password` | Informar: A senha do seu login. |


+ Request (application/json)

    + Body

            {   
                "username": "[usuário ja criado]",
                "password": "[password ja criado]"
            }

+ Response 200 (application/json)

    + Body

            {
                "user": {
                    "id": [id do usuário],
                    "username": [nome do usuário],
                    "password": "",
                    "role": [role do usuário]
                },
                "token": [access_token]
            }

# Group Recursos

# User [/api/v1/Account]
          
### Novo (Create) [POST/create]

+ Attributes (object)

    + username: nome do modulo (string, required)
    + id (int, optional) 
    + password (string,required)
    + role (string,optional)
    
    

+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

    + Body

            {
              "username": "caio",
              "password": "123456",
              "role": "Administrador"
            }

+ Response 200 (application/json)

    + Body

            {
                "id": 1,
                "username": "caio",
                "password": "123456",
                "role": "Administrador"
            }         


            
### Editar (Update) [PUT  /update/{id}]

Só pode alterar a 'Role' para Administrador, se não tiver nenhum cadastrado.

+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

    + Body

            {
                "id": 1,
                "username": "Vitor",
                "password": "0147",
                "role": ""
            }

+ Response 200 (application/json)
  Todos os dados do usuário

    + Body

            {
                "id": 1,
                "username": "Vitor",
                "password": "0147",
                "role": "Usuario"
            }



# Modulos [/api/v1/ModuloApi]

### Listar (List) [GET]

+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

+ Response 200 (application/json)

          {
            "id": 1,
            "nome": [Nome do Modulo],
            "qtdAulas": [Quantida de aulas cadastrads no Modulo],
            "aulas": null
        }
+ Response 401 (application/json)

          {
          }
          
### Novo (Create) [POST/create]

+ Attributes (object)

    + nome: nome do modulo (string, optional)
    + id (int, optional) 
    + qtdAulas (int,optioal)
    + aulas (array, optional) - aula
        + nome
        + moduloId
        + dataAula
        + modulo
    

+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

    + Body

            {
              "nome": ".Net"
            }

+ Response 200 (application/json)

    + Body

            {
                "id": 1,
                "nome": ".Net",
                "qtdAulas": 0,
                "aulas": null
            }         

### Detalhar (Read) [GET /{id}]

+ Parameters
    + id (required, number, `1`) ... Id do modulo

+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

+ Response 200 (application/json)
  Todos os dados do modulo

    + Body

            {
                "id": 1,
                "nome": ".Net",
                "qtdAulas": 0,
                "aulas": null
            }

+ Response 404 (application/json)
  Quando registro não for encontrado.

    + Body

            {
              "errCode": 404,
              "errMsg": "Nenhum registro com Id 1 econtrado",
              "errObs": null,
              "errFields": null
            }

+ Response 410 (application/json)
  Quando registro foi apagado do sistema, o Id de retorno é 410.

    + Body

            {
            }
            
### Editar (Update) [PUT  /update/{id}]

+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

    + Body

            {
                "id": 1,
                "nome": "C#",
                "qtdAulas": 0,
                "aulas": null
            }

+ Response 200 (application/json)
  Todos os dados do modulo

    + Body

            {
                "id": 1,
                "nome": "C#",
                "qtdAulas": 0,
                "aulas": null
            }

### Remover (Delete) [DELETE  /delete/{id}]

+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

+ Response 200 (application/json)

    + Body

            {  
            }
            
# Aulas [/api/v1/AulaApi]

### Listar (List) [GET]

+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

+ Response 200 (application/json)

          {
              "id": 1,
              "nome": "stringboot",
              "dataAula": "2022-01-13T00:13:35.388",
              "moduloId": 1,
              "modulo": {
                  "id": 1,
                  "nome": "Java",
                  "qtdAulas": 1,
                  "aulas": []
              }
          }
+ Response 401 (application/json)

          {
          }
          
### Novo (Create) [POST/create]

+ Attributes (object)

    + nome: nome da aula (string, optional)
    + id (int, optional) 
    + dataAula (string,optioal) - formato: YYYY-MM-DD
    + moduloId (int, required)
    + modulo (array, optional) - modulo
        + nome
        + id
        + aulas
        + qtdAulas
    

+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

    + Body

            {
              "nome": "Java SE 8",
              "moduloId":"1"
            }

+ Response 200 (application/json)

    + Body

            {
                "id": 2,
                "nome": "Java SE 8",
                "dataAula": "0001-01-01T00:00:00",
                "moduloId": 1,
                "modulo": null
            }         

### Detalhar (Read) [GET /{id}]

+ Parameters
    + id (required, number, `1`) ... Id do modulo

+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

+ Response 200 (application/json)
  Todos os dados da aula

    + Body

            {
                "id": 2,
                "nome": "Java SE 8",
                "dataAula": "0001-01-01T00:00:00",
                "moduloId": 1,
                "modulo": {
                    "id": 1,
                    "nome": "Java",
                    "qtdAulas": 2,
                    "aulas": []
                }
            }

+ Response 404 (application/json)
  Quando registro não for encontrado.

    + Body

            {
              "errCode": 404,
              "errMsg": "Nenhum registro com Id 1 econtrado",
              "errObs": null,
              "errFields": null
            }

+ Response 410 (application/json)
  Quando registro foi apagado do sistema, o Id de retorno é 410.

    + Body

            {
            }
            
### Editar (Update) [PUT  /update/{id}]

+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

    + Body

            {
                "id":2,
                "nome": "Java 10",
                "moduloId": 1
            }

+ Response 200 (application/json)
  Todos os dados da aula

    + Body

            {
                "id": 2,
                "nome": "Java 10",
                "dataAula": "0001-01-01T00:00:00",
                "moduloId": 1,
                "modulo": null
            }

### Remover (Delete) [DELETE  /delete/{id}]

+ Request (application/json)

    + Headers

            Authorization: Bearer [access_token]

+ Response 200 (application/json)

    + Body

            {  
            }            
