# PTwitch

## About

An interactive way for your Twitch Followers whatch your streams

Este git apenas a versão bem inicial do projeto PTwitch que resolvi compartilhar com vocês :)

Caso queiram utilizar este projeto para implementar do seu jeito e querer destribuir de forma comercial, sinta-se a vontade.

Se você tiver qualquer feedback sobre o código - Sua arquitetura, performance, etc - eu ficarei feliz de receber em meu email pedropereralourenco@gmail.com

## TwitchLib by swiftyspiffy

Para toda parte de interação com os serviços da Twitch foi utilizado a livraria TwitchLib, de swiftyspiffy. Meus agradecimentos para eles por facilitarem e realização dete projeto

TwitchLib:  https://github.com/TwitchLib

contato: swiftyspiffy@gmail.com

# The Backend 

## About

Você pode modificar toda parte de Backend do PTwitch localizado na pasta **TwitchChat_bckEnd/**

No Backend é onde acontece o registro de todas as atividades enviadas pelos usuários no chat. O programa deve registrar todos esses eventos dentro de um arquivo Json localizado no caminho definido em **Program.cs**

````csharp
public const string CredentialsJsonPath;
````

**note:** O arquivo Json é criado automaticamente quando você salva suas definições, quando está testando PTwitch na Unity Engine. Basta clicar no ícone de engranagem localizado no canto superior direito da tela.

###### The Json file will look like this
```json
{
    "UserName": "",
    "AccessToken": "",
    "Connected": false,
    "ReadMessages": true,
    "Followers": []
}
````

# The Frontend 

O projeto da Unity é onde acontece toda a parte visual do PTwitch. Desde o momento de login até a parte de vizualização do chat.
O projeto se encontra dendro da pasta **TwitchChat/**

## Setup Dev and Standalone Path 

A parte de Frontend precisa receber o mesmo caminho de onde está salvo o arquivo Json. Assim, PTwitch ficará recebendo as informações salvas e atualiazndo.

Como já dito antes, para definir esse caminho, na tela de Login, basta clicar na engrenagem e definir os valores. Não é necessário nenhum nome para o arquivo, somente passe o caminho onde você quer que seja salvo.

**note:** Existem dois cominhos onde você pode salvar o arquivo Json. O primeiro caminho serve para a versão standalone, e a segunda somente quando você está testando dentro da Unity Engine.
