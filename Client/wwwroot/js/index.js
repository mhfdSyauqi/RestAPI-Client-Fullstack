$.ajax({
    url: 'https://pokeapi.co/api/v2/pokemon/',
    success: function (result) {
        let listData;
        $.each(result.results, function (key, val) {
            listData += `
            <tr>
              <th scope="row">${key + 1}</th>
              <td>${val.name}</td>
              <td><button data-url="${val.url}" data-name="${val.name}" type="button" class="detail btn btn-primary" data-toggle="modal" data-target="#exampleModal">Detail</button></td>
            </tr>
             `
            $('#result').html(listData);
        })
    }
})



$('#exampleModal').on('show.bs.modal', function (event) {
    let element = $(event.relatedTarget);
    let modal = $(this);

  $.ajax({
        url: element.data('url'),
        success: function (result) {
            const { abilities, moves, name, species, sprites, types } = result;
            const gambar = sprites.other.dream_world.front_default;
            const detail = `
               <img src="${gambar}" alt="${name}" class="img-thumbnail" width="200">
               <h5>Types : ${types.map(res => `<span class="badge badge-info">${res.type.name}</span>`)}
               <h5>Moves : razor-wind , sword-dance , cut</h5>
            `
            modal.find('#exampleModalLabel').text(name.toUpperCase())
            modal.find('#m-body').html(detail);
        }
    })

   
})

