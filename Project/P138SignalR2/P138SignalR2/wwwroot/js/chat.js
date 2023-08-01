var connection = new signalR.HubConnectionBuilder().withUrl('/p138chat').build();

connection.start();

$('#sendButton').click(function () {
    let userName = $('#userInput').val();
    let message = $('#messageInput').val();

    connection.invoke('MesajGonder', userName, message);
})

connection.on('MesajQebulEden', function (u,m) {
    let li = `<li>
                    ${u}: ${m}
                </li>`;

    $('#messagesList').append(li);
})
////console.log(connection);

//$('#sendButton').click(function () {
//    connection.invoke("MesajGonder");

//})

//connection.on("MesajQebulEden", function (m) {
//    console.log(m)
//})


