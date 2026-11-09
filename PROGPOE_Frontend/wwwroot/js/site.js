// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
	document.getElementById("logBut").addEventListener("click", ()=>{
		var log = document.getElementById("logForm")
		var reg = document.getElementById("regForm")
		if (log.classList.contains("d-none")) {
			log.classList.add("d-block")
			reg.classList.add("d-none")
			log.classList.remove("d-none")
		}
	})
	document.getElementById("regBut").addEventListener("click", ()=>{
		var reg = document.getElementById("regForm")
		var log = document.getElementById("logForm")
		if (reg.classList.contains("d-none")) {
			reg.classList.add("d-block")
			log.classList.add("d-none")
			reg.classList.remove("d-none")
		}
	})