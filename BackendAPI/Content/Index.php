<!DOCTYPE html>
<html ng-app="application">
<head>
    <meta charset="utf-8" />
    <title>Test application</title>


    <script src="js/jquery-3.5.1.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script src="js/angular.min.js" type="text/javascript"></script>
    <script src="js/app.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/bootstrap-theme.min.css" />
    <script type="text/javascript">
function sendAjaxRequest(element,urlToSend,buttonaction) {
             var clickedButton = element;
              $.ajax({
                  type: "POST",
                  url: urlToSend,
                  data: { buttonid: clickedButton.attr('id'),actionid: buttonaction},
                  success:function(result){
      console.log('deu certo');
                  },
                 error:function(result)
                  {
                  alert('error');
                 }
             });
    
     }

        $(function(){
        $('#btn').click(function() {
        var column  = 5;
        var row = 3;
        var i = 0;
        for(ir = 0; ir < row; ir ++){
        for(ic = 0; ic < column; ic ++){
        var button = document.createElement('button');
        button.type = 'button';
        button.innerHTML = 'Press me ' + i;
        button.id = i;
        button.onclick = function() {
      
      sendAjaxRequest($(this),'/Abacate',1);
             
        };

        var container = document.getElementById('container');        
       
        container.appendChild(button);
        i++;
        }        
        var p = document.createElement ("br");
                   container.appendChild(p);
        }
        });
        });
    </script>
</head>
<body>
    <header><h4>Test application</h4></header>
    <div class="well-lg" ng-controller="testController">

        <div id='container'></div>
        <div>
            <input id="btn" type="button" value="button" />
        </div>
        <input type='button' onclick='alert(arrayToModify);' value='Show the buttons I have clicked' />
                </div>

        <footer></footer>
</body>
</html>