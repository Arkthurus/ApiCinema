const URL_BASE = "http://localhost:5231";
//Pegar todos os Filmes
export async function listFilms(){
	const filmesContainer = document.querySelector("#filmes-container");

	const response = await fetch(`${URL_BASE}/api/v1/Filmes`);
	const filmes   = await response.json();
	console.log(filmes);

	filmes.map((filme) => {
		const div           	= document.createElement("div");
		const titulo        	=  document.createElement("h2");
		const anoLancamento 	=  document.createElement("h3");
		const generos       	=  document.createElement("h3");
		const diretor       	=  document.createElement("h3");
		const atores   			=  document.createElement("h3");
		const notaIMDB  		=  document.createElement("h3");
  		const sinopse 			=  document.createElement("h3");

		titulo.innerText  		=  filme.titulo;
		anoLancamento.innerText = `Ano de Lançamento: ${filme.anoLancamento}`;
		generos.innerText		= `Generos: ${filme.generos}`;
		diretor.innerText 	    = `Diretor: ${filme.diretor.nome} | Data de Nascimento ${filme.diretor.dataNasc}`;
		console.log(filme.atores);
		if (filme.atores && filme.atores.length > 0) { //verifica se filme.atores n é NULL e se é maior q 0
			let atoresTexto     = "Atores:<br>";
			filme.atores.forEach((papel) => {//papel = atorPapelDTO
				atoresTexto    += `- ${papel.ator.nome} (Nascimento: ${papel.ator.dataNasc})<br>`;
			});
			atores.innerHTML    = atoresTexto;
		} else {
			atores.innerText    = "Atores: Nenhum ator informado";
		}

		notaIMDB.innerText      = `Nota IMBD: ${filme.notaIMDB}`;
		sinopse.innerText 		= `Sinopse: ${filme.sinopse}`;

		div.appendChild       (titulo);
		div.appendChild(anoLancamento);
		div.appendChild      (generos);
		div.appendChild  	 (diretor);
		div.appendChild       (atores);
		div.appendChild     (notaIMDB);
		div.appendChild      (sinopse);

		div.classList.add("filme-card");

		filmesContainer.appendChild(div);
	})
}
//Procurar Filme por Id
export async function filmSearch(id) {
try {
		const response = await fetch(`${URL_BASE}/api/v1/Filmes/${id}`);

		if (!response.ok) {
			// Se o status não for 2xx, retorna null
			console.warn(`Filme com id ${id} não encontrado. Status: ${response.status}`);
			return null;
		}

		const filme = await response.json();
		return filme;
	} catch (error) {
		console.error("Erro ao buscar o filme:", error);
		return null;
	}
}
//Exclui Filme por Id
export async function deleteFilm(id) {
	try {
		const response = await fetch(`${URL_BASE}/api/v1/Filmes/${id}`, {
			method: "DELETE",//informa ao Back q a requisição é DELETE, ja q o "GET" usa o mesmo URL
		});

		if (!response.ok) {//verifica se a "response" retornou FALSE e registra o erro
			console.warn(`Erro ao deletar o filme. Status: ${response.status}`);
			return false;
		}

		console.log(`Filme com ID ${id} deletado com sucesso.`);//Tudo deu certo e o FIlme foi excluido retornando TRUE pro FRONT
		return true;
	} catch (error) {
		console.error("Erro ao tentar deletar o filme:", error);
		return false;
	}
}
//Pegar todos os Diretores
export async function listDirectors(){
	const diretoresContainer = document.querySelector("#diretores-container");

	const response = await fetch(`${URL_BASE}/api/v1/Diretor`);
	const diretores   = await response.json();
	console.log(diretores);

	diretores.map((diretores) => {
		const div     	    = document.createElement("div");
		const nome    	    =  document.createElement("h2");
		const dataNasc 	    =  document.createElement("h3");
		const biografia	    =  document.createElement("h3");

		nome.innerText  	=  diretores.nome;
		dataNasc.innerText  = `Ano de Nascimento: ${diretores.dataNasc}`;
		biografia.innerText = `Biografia: ${diretores.biografia}`;

		div.appendChild     (nome);
		div.appendChild (dataNasc);
		div.appendChild(biografia);

		div.classList.add("filme-card");

		diretoresContainer.appendChild(div);
	})
}
//pegar todos os Atores
export async function listActors(){
	const atoresContainer = document.querySelector("#atores-container");

	const response = await fetch(`${URL_BASE}/api/v1/Ator`);
	const atores   = await response.json();
	console.log(atores);

	atores.map((ator) => {
		const div     	    = document.createElement("div");
		const nome    	    =  document.createElement("h2");
		const dataNasc 	    =  document.createElement("h3");
		const biografia	    =  document.createElement("h3");

		nome.innerText  	=  ator.nome;
		dataNasc.innerText  = `Ano de Nascimento: ${ator.dataNasc}`;


		div.appendChild     (nome);
		div.appendChild (dataNasc);

		div.classList.add("filme-card");

		atoresContainer.appendChild(div);
	})
}
//Pegar todos os Generos
export async function listGenres(){
	const generosContainer = document.querySelector("#generos-container");

	const response  = await fetch(`${URL_BASE}/api/v1/Genero`);
	const generos   = await response.json();
	console.log(generos);

	generos.map((genero) => {
		const div     	    = document.createElement("div");
		const nome    	    =  document.createElement("h2");

		nome.innerText  	=  genero.nome;
		
		div.appendChild     (nome);

		div.classList.add("filme-card");

		generosContainer.appendChild(div);
	})
}
// Função para coletar todos os gêneros selecionados
function coletarGenerosSelecionados() {
  // Cria um array vazio para armazenar os gêneros selecionados
  const generos = [];

  // Seleciona todos os elementos <select> com a classe "genero-select" na página
  document.querySelectorAll(".genero-select").forEach(select => {
    // Pega o valor selecionado naquele select
    const valor = select.value;

    // Verifica se o valor não está vazio e se ainda não foi adicionado ao array
    if (valor && !generos.includes(valor)) {
      // Adiciona o valor ao array de gêneros
      generos.push(valor);
    }
  });

  // Retorna o array com todos os gêneros selecionados, sem duplicatas
  return generos;
}

// Função para coletar os papeis (atores + papéis)
function coletarPapeis() {
  // Cria um array vazio para armazenar os papeis (atores e seus papéis)
const papeis = [];

// Seleciona todos os elementos <fieldset> dentro do container de papeis (#papeis-container)
const papeisContainers = document.querySelectorAll("#papeis-container fieldset");

// Para cada fieldset encontrado, executa a função
papeisContainers.forEach(fieldset => {
  // Pega o valor do input com a classe "ator-nome", remove espaços extras (trim)
  // O operador ?. evita erro se o elemento não existir, e ?? define valor padrão vazio
  const atorNome = fieldset.querySelector(".ator-nome")?.value.trim() ?? "";

  // Pega o valor do input com a classe "ator-dataNasc", sem trim porque é uma data
  const atorDataNasc = formatarData(fieldset.querySelector(".ator-dataNasc")?.value ?? "");

  // Pega o valor do input com a classe "papel", removendo espaços extras
  const papel = fieldset.querySelector(".papel")?.value.trim() ?? "";

  // Se todos os três campos tiverem valor (não vazios), adiciona ao array papeis
  if (atorNome && atorDataNasc && papel) {
    papeis.push({
      ator: {
        nome: atorNome,
        dataNasc: atorDataNasc
      },
      papel: papel
    });
	console.log(papeis);
  }
});

  return papeis;
}

// Monta o objeto filme para enviar para o backend
function montarObjetoFilme() {
  return {
    titulo: document.getElementById("titulo").value.trim(),
    anoLancamento: parseInt(document.getElementById("anoLancamento").value),
    sinopse: document.getElementById("sinopse").value.trim(),
    notaIMDB: parseFloat(document.getElementById("notaIMDB").value),
	preco: parseFloat(document.getElementById("preco").value),
    generos: coletarGenerosSelecionados(),
    diretor: {
      nome: document.querySelector(".diretor-nome")?.value.trim() ?? "",
      dataNasc: formatarData(document.querySelector(".diretor-dataNasc")?.value ?? ""),
      biografia: document.querySelector(".diretor-biografia")?.value.trim() ?? ""
    },
    papeis: coletarPapeis()
  };
}

// Função para limpar o formulário após salvar
function limparFormulario() {
  // Limpa inputs filme
  document.getElementById("titulo").value = "";
  document.getElementById("anoLancamento").value = "";
  document.getElementById("sinopse").value = "";
  document.getElementById("notaIMDB").value = "";

  // Limpa diretor
  document.querySelector(".diretor-nome").value = "";
  document.querySelector(".diretor-dataNasc").value = "";
  document.querySelector(".diretor-biografia").value = "";

  // Remove todos os papeis (fieldsets) de atores
  document.querySelectorAll("#papeis-container fieldset").forEach(el => el.remove());

  // Remove todos os gêneros selecionados
  document.getElementById("generos-container").innerHTML = "";


}

// Função que envia o filme para o backend via fetch
export async function saveRef() {
  try {
    const filmeObj = montarObjetoFilme();

    if (!filmeObj.titulo) {
      alert("Por favor, preencha o título do filme.");
      return;
    }
    if (filmeObj.generos.length === 0) {
      alert("Adicione ao menos um gênero.");
      return;
    }

    const response = await fetch(`${URL_BASE}/api/v1/Filmes`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(filmeObj)
    });

    if (!response.ok) {
      const erro = await response.text();
      throw new Error("Erro ao cadastrar filme: " + erro);
    }
	console.log(filmeObj);
    alert("Filme cadastrado com sucesso!");
    limparFormulario();
  } catch (error) {
    alert(error.message);
  }
}
function formatarData(dataString){
	const data = new Date(dataString);

	const dia = String(data.getUTCDate()).padStart(2, '0');
	const mes = String(data.getMonth() + 1).padStart(2, '0');
	const ano = String(data.getFullYear());

	const formatada = `${dia}-${mes}-${ano}`;
	return formatada;
}
