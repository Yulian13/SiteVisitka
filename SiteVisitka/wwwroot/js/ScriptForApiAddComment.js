const ErrorText = document.getElementById("ErrorText");
const ErrorName = document.getElementById("ErrorName");
const statusOK = document.getElementById("OkStatus");
const form = document.forms["newComment"];
const name = form.elements["Name"];
const address = form.elements["Address"];
const text = form.elements["Text"];

async function PostComment() {

	var path = '/api/Comment';
	var work = {
		Name: name.value,
		Address: address.value,
		Text: text.value,
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
			if (errorData["text"]) {
				ErrorText.style.display = "block";
			}
			else {
				ErrorText.style.display = "none";
			}

			if (errorData["name"]) {
				ErrorName.style.display = "block";
			}
			else {
				ErrorName.style.display = "none";
			}
		}
	}
}

function reset() {
	ErrorName.style.display = "none";
	ErrorText.style.display = "none";
	name.value = "";
	address.value = "";
	text.value = "";
}