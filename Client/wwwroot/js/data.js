$(document).ready(function () {
	$('#myTable').DataTable({
		ajax: {
			url: 'https://localhost:44365/employees/getall',
			dataSrc: ''
		},
		columns: [
			{
				data: null,
				"render": function (data, type, full, meta) {
					return meta.row + 1;
				}
			},
			{ data: 'nik' },
			{
				data: '',
				render: function (data, type, row) {
					return `${row.firstName} ${row.lastName}`;
				}
			},
			{
				data: '',
				orderable: false,
				render: function (data, type, row) {
					return row['phone'].replace("0", "+62")
				}
			},
			{
				data: 'salary',
				render: DataTable.render.number('.', ',', 0, 'Rp. ')
			},
			{
				data: null,
				className: "dt-center",
				render: function (data, type, row, meta) {
					return `<button type="button" class="btn btn-warning"><i class="bi bi-pencil"></i></button>
                  <button type="button" class="btn btn-danger"><i class="bi bi-trash"></i></button>`
				}
			}
		],
		dom: 'Bfrtip',
		buttons: [
			'excel', 'pdf'
		]
	});

	$('#registerBtn').click(function (e) {
		e.preventDefault();
		InsertData();
	});
});



function InsertData() {

	let obj = {
		"nik": $("#inputNIK").val(),
		"firstName": $("#firstName").val(),
		"lastName": $("#lastName").val(),
		"phone": $("#phone").val(),
		"email": $("#email").val(),
		"salary": +$("#salary").val(),
		"birthDate": $("#bod").val(),
		"password": $("#password").val(),
		"degree": $("#degree").val(),
		"gpa": $("#gpa").val(),
		"universityId": +$("#univ").val()
	}

	console.log(obj);
	$.ajax({
		url: ('https://localhost:5002/employees/register'),
		//type: "POST",
		//data: obj,
		headers: {
			'Accept': 'application/json',
			'Content-Type': 'application/json'
		},
		'type': 'POST',
		'data': { entity : obj }, //objek kalian
		'dataType': 'json',
	}).done((result) => {
		//buat alert pemberitahuan jika success
		Swal.fire({
			title: 'Berhasil Menambahkan data',
			icon: 'success'
		})
		$('#myTable').DataTable().ajax.reload();
		resetModal();
	}).fail((error) => {
		//alert pemberitahuan jika gagal
		Swal.fire({
			title: 'Gagal Menambahkan Data',
			icon: 'error'
		})
		console.log(error)
	});
}

function resetModal() {
	$('#registerModal').modal('hide');
	$("#inputNIK").val("");
	$("#firstName").val("");
	$("#lastName").val("");
	$("#phone").val("");
	$("#email").val("");
	$("#salary").val("");
	$("#bod").val("");
	$("#password").val("");
	$("#degree").val("");
	$("#gpa").val("");
	$("#univ").val("");
};