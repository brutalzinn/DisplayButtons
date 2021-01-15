
    function ClearBackground(picID) {
        console.log('cleaning packet');
            var pic = document.getElementById(picID);
            pic.src = "";
        }

        function atualizarDefinicao() {
        $.ajax(
            {
                type: 'GET',
                url: '/api/Button',
                dataType: 'html',
                cache: false,
                async: true,
                success: function (data) {
                    $('#definicaoArquitetura').html(data);
                }
            });
        }




        function sendAjaxRequest(element, urlToSend, buttonaction) {
        var clickedButton = element;
        $.ajax({
            type: "GET",
            url: urlToSend,
            data: {id: clickedButton.attr('value'), actionbutton: buttonaction },
            success: function (result) {
            console.log('deu certo');
            },
            error: function (result) {
            alert('error');
            }
        });

    }


