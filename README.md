# SavvyFix - Precificação dinâmica
Enzo Oliveira  - RM 551356
João Vitor  - RM 550381
Pedro  - RM 551446
Matheus - RM 99572
Igor - RM 550415

<br>

## Arquitetura do sistema
![Arquitetura_Dotnet](https://github.com/user-attachments/assets/abef2011-e2b8-4e96-bb5a-8b84216a8dce)

<p>A arquitetura utilizada segue uma abordagem monolítica, ou seja, todas as funcionalidades desenvolvidas estão em interconectados e dependem uns dos outros.</p> 

<p>Como a Savvyfix é um projeto pequeno e com mais simplicidade, a abordagem monolítica promove uma maior eficiência no desenvolvimento inicial por conta dos seguintes pontos:</p>

<p>&nbsp;&nbsp;&nbsp;&nbsp;1. O serviço de aplicativo na Azure com a aplicação de c# é centralizado com um único serviço de aplicativo que contém várias responsabilidades, como Spring Boot e REST. Isso é útil para trazer mais facilidade no gerenciamento e integração, diminuindo riscos de falhas.</p>

<p>&nbsp;&nbsp;&nbsp;&nbsp;2. Com a utilização do banco de dados Oracle é centralizado para todas as aplicações, como Kotlin e Azure, a solução se torna mais acoplada. As vantagens dessa utilização é a diminuição das inconsistências de dados, fator importante para a precificação dinâmica.</p>

<p>&nbsp;&nbsp;&nbsp;&nbsp;3. As funcionalidades desenvolvidas não possuem divisão de responsabilidade entre os serviços, pois as operações convergem para o banco de dados e o serviço de aplicativo. Como o projeto é pequeno, isso é importante para diminuir a sobrecarga de comunicação entre muitos sistemas via APIs.</p>

<p>&nbsp;&nbsp;&nbsp;&nbsp;4. Caso haja uma alteração em uma aplicação, todas as outras aplicações são impactadas e devem ser recompiladas por estarem conectadas e dividirem a mesma modelagem. Essa abordagem garante a consistência entre todos os sistemas, pois, ao ser realizado uma alteração, será necessário conferir em todas as outras aplicações se não há mais codificações inconsistentes.</p>

<br>

## Design Patterns - Service Layer
<p>A Service Layer (Camada de Serviço) é um padrão arquitetural que separa a lógica de negócios da lógica de apresentação e de persistência de dados. Ela centraliza a lógica de operações do sistema em classes de serviços, facilitando a manutenção e escalabilidade do código.</p>
<p>Em nosso projeto foi criada uma camada Service para o tratamento de precificação e manipulação dos dados antes de serem registrados em nosso banco de dados, garantindo assim uma organização do código e
segurança no tratamento das informações passadas pelo usuário.</p>


## Instruções para rodar a API

### Realizar o clone do projeto 

#### <p>O primeiro passoa para testar a apliação é realizar o git clone do nosso projeto através do linK: https://github.com/natevitor/C-Sp4x.git</p>


<p>1. Abra um terminal e digite: git clone https://github.com/natevitor/C-Sp4x.git/p>
<p>2. Abra o projeto em uma IDE destinada para C#/.NET como Rider ou Visual Studio.</p>
<p>3. Inicie o projeto SavvyfixAspNet.Api como http.</p>
<p>4. Após iniciar, entre no link abaixo para testar a aplicação através do Swagger UI: </p>
http://localhost:5255/swagger/index.html

<br>

## Teste da API

![image](https://github.com/user-attachments/assets/ab5e23eb-8813-431c-b073-2a93d63fbf68)

<p>Essa é a página inicial da documentação do projeto, para realizar testes basta apenas clicar na seta. </p>

![image](https://github.com/user-attachments/assets/f59d75ba-beb7-4e33-b76b-d4e2b95735a8)

<p>Depois em "Try it out". </p>

![image](https://github.com/user-attachments/assets/a8b7eceb-a5ba-481d-9cc3-7153338cd470)

<p>Execute e temos nossa resposta da API.</p>

![image](https://github.com/user-attachments/assets/72b8137e-0b6c-4c92-bda8-07b00eff68ca)

<p>Para fazer uma inserção, vá em POST e altere o JSON para inserir as informações que deseja cadastrar no banco de dados.</p>

![image](https://github.com/user-attachments/assets/7183eeee-d834-470f-8af5-ee600f31862b)

<p>Para editar, vá em PUT e informe o id e os dados que serão alterados.</p>

![image](https://github.com/user-attachments/assets/47cb5a01-3935-463b-8503-663631a530f3)

<p>Para deletar, vá em DELETE e informe o id.</p>

![image](https://github.com/user-attachments/assets/0c45ac38-3412-469a-ae3f-9d6b584545f3)








