import { useEffect } from "react";

function ListarProdutos() {
  //useEffect é utilizado para executar qualquer código
  //no momento em que o componente estiver sendo aberto
  useEffect(() => {
    //AXIOS para realizar as requisições    
    fetch("https://viacep.com.br/ws/80020010/json/")
      .then(resposta => {
        return resposta.json();
      })
      .then(dados => {
        console.log(dados);
      });
  }, []);

  return (
    <div>
      <h1>Listar Produtos</h1>
    </div>
  );
}

export default ListarProdutos;

//Mostrar os dados do endereço na página da aplicação
// - Estados

//Realizar a requisição para a sua API
// - Verificar o erro de CORS da API
// -