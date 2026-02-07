var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.UseStaticFiles();

// API endpoint to handle name input
app.MapPost("/submit-name", (NameInput input) =>
{
    return new { message = $"Your name is {input.Name}" };
});

// Serve HTML page
app.MapGet("/", () => Results.Content(@"
<!DOCTYPE html>
<html>
<head>
    <title>Name Input App</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            max-width: 600px;
            margin: 50px auto;
            padding: 20px;
        }
        input {
            padding: 10px;
            font-size: 16px;
            width: 300px;
        }
        button {
            padding: 10px 20px;
            font-size: 16px;
            background-color: #007bff;
            color: white;
            border: none;
            cursor: pointer;
        }
        button:hover {
            background-color: #0056b3;
        }
        #result {
            margin-top: 20px;
            font-size: 20px;
            color: green;
        }
    </style>
</head>
<body>
    <h1>Enter Your Name</h1>
    <input type='text' id='nameInput' placeholder='Type your name here' />
    <button onclick='submitName()'>Submit</button>
    <div id='result'></div>

    <script>
        function submitName() {
            const name = document.getElementById('nameInput').value;
            
            fetch('/submit-name', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ name: name })
            })
            .then(response => response.json())
            .then(data => {
                document.getElementById('result').innerText = data.message;
            });
        }
    </script>
</body>
</html>
", "text/html"));

app.Run();

record NameInput(string Name);