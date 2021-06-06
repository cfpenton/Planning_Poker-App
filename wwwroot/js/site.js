

$(() => {
  LoadVotesData();

  var connection = new signalR.HubConnectionBuilder()
    .withUrl("/votesHub")
    .build();
  connection.start();
  connection.on("LoadVotes", function () {
    LoadVotesData();
  });
  LoadVotesData();

  function LoadVotesData() {
    var tr = '';

    $.ajax({
      url: '/AllVotes/GetVotes',
      method: 'GET',
      success:(result) => {
          $.each(result, (k, v) => {
          tr += `<tr>
                   <td>${v.user_id}</td>
                   <td>${v.card_id}</td>
                   <td>${v.story_id}</td>
                   <td></td>
                   <td>
                   <a href='../AllVotes/Edit?id=${v.id}'>Edit</a> |
                   <a href='../AllVotes/Details?id=${v.id}'>Details</a> |
                   <a href='../AllVotes/Delete?id=${v.id}'>Delete</a>
                   </td>
                   </tr>`
        })
        $("#tableBody").html(tr);
    },
     error: (error) => {
         console.log (error)
     }
    });
  }
})
