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

		filmesContainer.appendChild(div);
	})
}
//Procurar Filme por Id
export async function filmSearch(id) {
	const response = await fetch(`${URL_BASE}/api/v1/Filmes/${id}`);
	const filme    = await response.json();
	return filme;
}

//Pegar todos os Diretores
export async function listDirectors(){
	const diretoresContainer = document.querySelector("#diretores-container");

	const response = await fetch(`${URL_BASE}/api/v1/Diretores`);
	const diretores   = await response.json();
	console.log(diretores);

	diretores.map((diretores) => {
		const div     	    = document.createElement("div");
		const nome    	    =  document.createElement("h2");
		const dataNasc 	    =  document.createElement("h3");
		const biografia	    =  document.createElement("h3");

		nome.innerText  	=  diretores.nome;
		dataNasc.innerText  = `Ano de Nascimento: ${diretores.dataNasc}`;
		biografia.innerText = `Biografia: ${diretores.Biografia}`;

		div.appendChild     (nome);
		div.appendChild (dataNasc);
		div.appendChild(biografia);

		diretoresContainer.appendChild(div);
	})
}
//pegar todos os Atores
export async function listActors(){
	const atoresContainer = document.querySelector("#atores-container");

	const response = await fetch(`${URL_BASE}/api/v1/Atores`);
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


		atoresContainer.appendChild(div);
	})
}
export async function listGenres(){
	const generosContainer = document.querySelector("#generos-container");

	const response  = await fetch(`${URL_BASE}/api/v1/Generos`);
	const generos   = await response.json();
	console.log(generos);

	generos.map((genero) => {
		const div     	    = document.createElement("div");
		const nome    	    =  document.createElement("h2");

		nome.innerText  	=  genero.nome;
		
		div.appendChild     (nome);

		generosContainer.appendChild(div);
	})
}