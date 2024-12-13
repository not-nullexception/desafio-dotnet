# Visão Geral do projeto

Olá, a ideia desse projeto foi criar algo simples mas com alguma sofisticação.

O projeto tem apenas uma controller, onde uso para cadastrar novos produtos e listar os produtos, assim como pedia o desafio.

Algumas partes deste projeto, poderiam ser dividas em outras DLLs, porém, pra evitar muita complexidade, optei por construir tudo no mesmo projeto e dividir em pastas.

As migrations são executadas de forma automática na inicialização do projeto.


## Imporante
1. Na minha máquina ao invés do docker eu uso o podman (um tipo de docker open source), em teoria é pra funcionar da mesma forma! mas caso tenha alguma problema, só avisar.
2. Nunca usei o CI do Github (apenas do Azure Devops, Pipeline do GCP e usei a muito tempo um pouco do Jenkins), fiz um CI apenas com um estudo rapido aqui de como ela funciona, espero que atenda a expectativas.
3. Frontend feito com React + Vite super simples, apenas para testar a solução mesmo, sem gerenciamento de estado, componentes nem nada demais.

### Resumo de cadas pasta

1. Controllers: Gerenciar requisições e respostas HTTP.

2. Data: Gerenciar conexões com o banco de dados e configurações de entidades.

3. Services: Encapsular a lógica de negócio e coordenar entre controllers e repositórios.

4. Repositórios: Gerenciar o acesso e a persistência de dados.

5. DTOs: Facilitar a troca de dados entre diferentes camadas, especialmente entre a API e seus consumidores.

6. Models: Representar as estruturas de dados principais da aplicação.

7. Middleware: Contem todos os middlewares da aplicação, no caso criei apenas um para tratar exções.

## Decisões de Design

1. Arquitetura em Camadas
Adotar uma arquitetura em camadas garante a separação de responsabilidades, tornando a aplicação mais fácil de gerenciar, testar e escalar.

2. Padrão Repository
Utilizar o Padrão Repository abstrai a lógica de acesso a dados, permitindo testes mais fáceis e a flexibilidade de trocar fontes de dados sem afetar a lógica de negócio.

3. Camada de Serviço
A Camada de Serviço encapsula a lógica de negócio, garantindo que os controllers permaneçam leves e focados apenas em gerenciar requisições e respostas HTTP.

4. DTOs
Usados para controlar o fluxo de dados entre o servidor e os clientes para expor apenas informações necessárias e aumentando a performance ao reduzir o tamanho das cargas úteis.

5. AutoMapper
O AutoMapper é utilizado para facilitar o mapeamento entre models e DTOs.

6. Validação
Optei por simples de modelo (por exemplo, [Required], [Range]).

7. Testes com xUnit e Moq
Para os testes optei por usar o xUnit com Moq para facilita a criação de objetos mock, permitindo testes unitários isolados sem dependências externas.