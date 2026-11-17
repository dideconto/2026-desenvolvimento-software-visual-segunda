import { useEffect, useState } from "react";
import { Produto } from "./models/Produto";

function ListarProdutos() {
  //Estados/Variáveis
  const [produtos, setProdutos] = useState<Produto[]>([]);

  //useEffect é utilizado para executar qualquer código
  //no momento em que o componente estiver sendo aberto
  useEffect(() => {
    //AXIOS para realizar as requisições
    fetch("http://localhost:5195/api/produto/listar")
      .then((resposta) => {
        return resposta.json();
      })
      .then((produtos) => {
        console.table(produtos);
        setProdutos(produtos);
        // setLocalidade(dados.localidade);
        // setLogradouro(dados.logradouro);
      });
  }, []);

  return (
    <div>
      <h1>Lista de Produtos</h1>

      <table>
        <thead>
          <tr>
            <th>#</th>
            <th>Nome</th>
            <th>Valor</th>
            <th>Criado Em</th>
          </tr>
        </thead>
        <tbody>
          {produtos.map((produto) => (
            <tr>
              <td>{produto.id}</td>
              <td>{produto.nome}</td>
              <td>{produto.valor}</td>
              <td>{produto.criadoEm}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ListarProdutos;

//Mostrar os dados do endereço na página da aplicação
// - Estados

//Realizar a requisição para a sua API
// - Verificar o erro de CORS da API
// -
