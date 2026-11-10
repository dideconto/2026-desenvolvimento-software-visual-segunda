import { useEffect, useState } from "react";

function ListarProdutos() {
  //Estados/Variáveis
  // const [localidade, setLocalidade] = useState("");
  // const [logradouro, setLogradouro] = useState("");
  const [produtos, setProdutos] = useState([]);

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
      <ul>
        {produtos.map((produto: any) => (
          <li>{produto.nome}</li>
        ))}
      </ul>
    </div>
  );
}

export default ListarProdutos;

//Mostrar os dados do endereço na página da aplicação
// - Estados

//Realizar a requisição para a sua API
// - Verificar o erro de CORS da API
// -
