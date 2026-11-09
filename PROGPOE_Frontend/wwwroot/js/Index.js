// JavaScript for Index view
	document.getElementById("logBut").addEventListener("click", ()=>{
		var log = document.getElementById("logForm")
		el.style.display = "block"
		var reg = document.getElementById("regForm")
		el.style.display = "hidden"

	})
	document.getElementById("regBut").addEventListener("click", ()=>{
		var reg = document.getElementById("regForm")
		el = "block"
		var log = document.getElementById("logForm")
		el.style.display = "hidden"
	})