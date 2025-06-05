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
export async function filmSearch(id) {
	const response = await fetch(`${URL_BASE}/api/v1/Filmes/${id}`);
	const filme    = await response.json();
	return filme;
}
// listFilms();
// //Pegar todos os Diretores
// async function listDirectors(){
// 	const response  = await fetch(`${URL_BASE}/api/v1/Diretores`);
// 	const diretores = await response.json();
// 	console.log(diretores);
// }
// listDirectors();
// async function listActors(){
// 	const response = await fetch(`${URL_BASE}/api/v1/Atores`);
// 	const atores   = await response.json();
// 	console.log(atores);
// }
// listActors();
// async function listGenres(){
// 	const response = await fetch(`${URL_BASE}/api/v1/Generos`);
// 	const generos  = await response.json();
// 	console.log(generos);
// }
// listGenres();
// function saveRef(){



// }