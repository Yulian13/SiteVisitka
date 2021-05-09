const ErrorUrls = document.getElementById("ErrorUrls");
const ErrorName = document.getElementById("ErrorName");
const statusOK = document.getElementById("statusOK");
const form = document.forms["Test"];
const name = form.elements["Name"];
const description = form.elements["Description"];
const address = form.elements["Address"];
const text = form.elements["urlImages"];
const prestige = form.elements["Prestige"];

async function CreatWork() {

	var path = '/api/Work';
	var work = {
		Name: name.value,
		Description: description.value,
		Address: address.value,
		urlImages: text.value,
		Prestige: Boolean(prestige.value)
	};
	
	var obj1 = JSON.stringify(work);
	
	const respons = await fetch(
		path, 
		{
			method: "POST",
			headers: { "Accept": "application/json", "Content-Type": "application/json" },
			body: obj1
		}
	);

	if (respons.ok === true) {
		statusOK.style.display = "block"
		reset();
	}
	else {
		statusOK.style.display = "none";
		const errorData = await respons.json();
		console.log("errors", errorData);

		if (errorData) {
			if (errorData["urls"]) {
				ErrorUrls.textContent = errorData["urls"];
			}
			else {
				ErrorUrls.textContent = "";
			}

			if (errorData["name"]) {
				ErrorName.textContent = errorData["name"];
			}
			else {
				ErrorName.textContent = "";
			}
		}
	}
}

function reset() {
	prestige.value = false;
	name.textContent = "";
	description.value = "";
	address.value = "";
	text.value = "";
	ErrorUrls.textContent = "";
	ErrorName.textContent = "";
}
