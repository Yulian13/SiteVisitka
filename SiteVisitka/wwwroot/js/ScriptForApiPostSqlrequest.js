const statusError = document.getElementById("statusError");
const statusOK = document.getElementById("statusOK");
const form = document.forms["PostRequest"];
const request = form.elements["request"];

async function OnPostRequest() {

	var path = '/api/Work';
	var obj = {
		SqlRequest: request.value
	};

	var obj1 = JSON.stringify(obj);

	const respons = await fetch(
		path,
		{
			method: "PUT",
			headers: { "Accept": "application/json", "Content-Type": "application/json" },
			body: obj1
		}
	);

	if (respons.ok === true) {
		statusOK.style.display = "block"
		statusError.style.display = "none";
	}
	else {
		statusOK.style.display = "none";
		statusError.style.display = "block";
		console.log("errors");
	}
}
