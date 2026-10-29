# API de Folha de Pagamento

API Minimal em C#/.NET 8 para cadastrar e consultar folhas de pagamento. Os dados são armazenados em SQLite no arquivo `Deconto_Diogo.db`.

## Executar

Na pasta `Diogo`, execute:

```powershell
dotnet restore
dotnet run
```

O banco e a tabela são criados automaticamente na primeira execução. Para exemplos de chamadas, use `Tests/FolhaPagamento.http` com a extensão REST Client do VS Code.

## Endpoints

- `POST /api/folha/cadastrar` — cadastra uma folha e calcula salário bruto, IR, INSS, FGTS e salário líquido.
- `GET /api/folha/listar` — lista as folhas cadastradas.
- `GET /api/folha/buscar/{cpf}/{mes}/{ano}` — busca uma folha.
- `DELETE /api/folha/remover/{cpf}/{mes}/{ano}` — remove uma folha.
- `GET /api/folha/total-liquido` — soma os salários líquidos.

O cadastro valida somente mês (de 1 a 12), ano (a partir de 2000) e duplicidade de CPF/mês/ano. O CPF é armazenado como recebido. O FGTS é calculado como 8% do salário bruto e não reduz o salário líquido.
