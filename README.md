# Financial-Controller

## Objetivo:

Implementar um sistema de controle de gastos residenciais.
Deixar claro qual foi a lógica/função do que foi desenvolvido, através de comentários e documentação no próprio código. 

 
### Especificação:

Em linhas gerais, basta que o sistema cumpra os requisitos apresentados. É importante que o sistema seja separado entre webApi e front.

 
### Tecnologias (obrigatório):

Back-end: C# e .Net.
Front-end: React com typescript.
Persistência: Fica a sua escolha, mas os dados devem se manter após reiniciar o sistema. 

 
### Funcionalidades:

#### Cadastro de `Pessoas`: 

Deverá ser implementado um cadastro contendo as funcionalidades básicas de gerenciamento: criação, edição, deleção e listagem.

Em casos que se delete uma pessoa, todas a transações dessa pessoa deverão ser apagadas.

O cadastro de `Pessoa` deverá conter:

    Identificador (deve ser um valor único gerado automaticamente);
    Nome (texto com tamanho máximo de 200);
    Idade;


#### Cadastro de `Categoria`: 

Deverá ser implementado um cadastro contendo as funcionalidades básicas de gerenciamento: criação e listagem.

O cadastro de categoria deverá conter:

    Identificador (deve ser um valor único gerado automaticamente);
    Descrição (texto com tamanho máximo de 400);
    Finalidade (despesa/receita/ambas)


#### Cadastro de `Transações`: 

Deverá ser implementado um cadastro contendo as funcionalidades básicas de gerenciamento: criação e listagem.

Caso o usuário informe um menor de idade (menor de 18), apenas despesas deverão ser aceitas.

O cadastro de `Transação` deverá conter:

    Identificador (deve ser um valor único gerado automaticamente);
    Descrição (texto com tamanho máximo de 400);
    Valor (número positivo);
    Tipo (despesa/receita);
    Categoria: restringir a utilização de categorias conforme o valor definido no campo finalidade. Ex.: se o tipo da transação é despesa, não poderá utilizar uma categoria que tenha a finalidade receita.
    Pessoa (identificador da pessoa do cadastro anterior);


#### Consulta de totais por pessoa:

Deverá listar todas as pessoas cadastradas, exibindo o total de receitas, despesas e o saldo (receita – despesa) de cada uma.
Ao final da listagem anterior, deverá exibir o total geral de todas as pessoas incluindo o total de receitas, total de despesas e o saldo líquido.


#### Consulta de totais por categoria (opcional):

Deverá listar todas as categorias cadastradas, exibindo o total de receitas, despesas e o saldo (receita – despesa) de cada uma.

Ao final da listagem anterior, deverá exibir o total geral de todas as categorias incluindo o total de receitas, total de despesas e o saldo líquido.


## Especificações da Implementação
### Diagrama ER

```mermaid
erDiagram

    %% =========================
    %% ENTIDADE: USUARIO (TENANT)
    %% =========================
    USUARIO {
        GUID Id PK "Identificador único"
        string Nome
        string Email "Único (login)"
        string SenhaHash "Senha criptografada"
        datetime DataCriacao
    }

    %% =========================
    %% ENTIDADE: PESSOA
    %% =========================
    PESSOA {
        GUID Id PK
        string Nome "Máx 200 caracteres"
        int Idade
        GUID UsuarioId FK "Dono dos dados"
    }

    %% =========================
    %% ENTIDADE: CATEGORIA
    %% =========================
    CATEGORIA {
        GUID Id PK
        string Descricao "Máx 400 caracteres"
        string Finalidade "DESPESA | RECEITA | AMBAS"
        GUID UsuarioId FK
    }

    %% =========================
    %% ENTIDADE: TRANSACAO
    %% =========================
    TRANSACAO {
        GUID Id PK
        string Descricao "Máx 400 caracteres"
        decimal Valor "Somente positivo"
        string Tipo "DESPESA | RECEITA"
        GUID PessoaId FK
        GUID CategoriaId FK
        GUID UsuarioId FK "Isolamento por usuário"
        datetime Data "Data da transação"
    }

    %% =========================
    %% RELACIONAMENTOS
    %% =========================

    USUARIO ||--o{ PESSOA : "possui"
    USUARIO ||--o{ CATEGORIA : "possui"
    USUARIO ||--o{ TRANSACAO : "possui"

    PESSOA ||--o{ TRANSACAO : "realiza"
    CATEGORIA ||--o{ TRANSACAO : "classifica"

    %% =========================
    %% REGRAS DE NEGÓCIO
    %% =========================

    %% 1. AUTENTICAÇÃO:
    %% Usuário realiza login via Email + Senha
    %% Sistema retorna JWT contendo UsuarioId

    %% 2. ISOLAMENTO:
    %% Toda query deve filtrar por UsuarioId (extraído do token)

    %% 3. CONSISTÊNCIA:
    %% TRANSACAO.UsuarioId == PESSOA.UsuarioId == CATEGORIA.UsuarioId

    %% 4. EXCLUSÃO EM CASCATA:
    %% Ao excluir PESSOA → remover TRANSACOES associadas

    %% 5. MENOR DE IDADE:
    %% Se PESSOA.Idade < 18:
    %%    TRANSACAO.Tipo = DESPESA

    %% 6. VALIDAÇÃO DE CATEGORIA:
    %% DESPESA → categoria (DESPESA ou AMBAS)
    %% RECEITA → categoria (RECEITA ou AMBAS)

    %% 7. VALOR:
    %% TRANSACAO.Valor > 0

    %% =========================
    %% CONSULTAS
    %% =========================

    %% Sempre filtradas por UsuarioId
```