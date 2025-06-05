const API_URL = "http://localhost:5231";
function listRef(){
	fetch(API_URL)
		.then(res => res.json)
		.then(data => {
			const list = document.getElementsById("refList");
			list.innerHTML = "";
			data.forEach(ref => {
				const li = document.createElement("li");
				li.textContent = '${ref.Titulo}
				(${ref.AnoLancamento}) -';
			})
		})
}
function saveRef(){



}