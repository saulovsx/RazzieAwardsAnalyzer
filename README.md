### **Golden Raspberry Awards API**
Esta API RESTful permite a leitura da lista de indicados e vencedores da categoria Pior Filme do Golden Raspberry Awards.
Retorna coleções de dados contendo o produtor com maior intervalo entre dois prêmios consecutivos, e o que
obteve dois prêmios mais rápido.

**Arquitetura e Práticas de Programação**

Este projeto foi desenvolvido seguindo os princípios da **Clean Architecture**, visando a separação de preocupações, a independência de frameworks, e a facilidade de manutenção e testabilidade.
Foram adotados os princípios SOLID e práticas de Clean Code ao longo do desenvolvimento para garantir que o código seja robusto, fácil de entender e manter.

**Tecnologias e Ferramentas**

- .NET 8: Framework utilizado para o desenvolvimento da API.
- Entity Framework In-Memory: Banco de dados em memória utilizado para testes e armazenamento de dados.
- CSV File: O arquivo CSV para importação dos dados, deve estar dentro da pasta **FileCSV**. A pasta de ser copiada no mesmo diretório do executável do projeto RazzieAwardsAnalyzer.API.
  Exemplo: `C:\MyProjects\RazzieAwardsAnalyzer\Presentation\RazzieAwardsAnalyzer.API\bin\Debug\net8.0\FileCSV`.
- Testes de Integração: Implementados com xUnit e Moq.

**Configuração do Projeto**

- Clone o repositório para a sua máquina local usando o comando:
 `git clone https://github.com/saulovsx/RazzieAwardsAnalyzer.git`
- Abra a solução do projeto com o Visual Studio.
- Certifique-se de que o projeto está configurado para usar o .NET 8.
- Coloque o arquivo CSV contendo os dados para importação na pasta FileCSV na raiz do projeto.
- No Visual Studio, configure o projeto **RazzieAwardsAnalyzer.API** como o projeto de inicialização.
- Executando a Aplicação

**Para executar os testes de integração:**

- CSV File: O arquivo CSV para importação dos dados, deve estar dentro da pasta **FileCSV**. A pasta de ser copiada no mesmo diretório do executável do projeto teste. Exemplo: `C:\MyProjects\RazzieAwardsAnalyzer\Tests\RazzieAwardsAnalyzer.Test\bin\Debug\net8.0\FileCSV`.
- No Visual Studio, abra o Test Explorer.
- Encontre os testes de integração, que estão localizados no projeto de testes que usa xUnit.
- Execute os testes através do Test Explorer.
- Os resultados dos testes serão exibidos no Test Explorer após a conclusão dos mesmos.
