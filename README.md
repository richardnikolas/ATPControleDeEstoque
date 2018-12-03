# Projeto II - ATP - Controle de Estoque

<h2> Controle de Estoque </h2>

<p> Um mercadinho de uma pequena cidade do interior de Minas Gerais deseja fazer um controle mínimo do seu estoque de produtos. A cada produto está associada uma descrição (nome), a quantidade em estoque, a quantidade mínima para o estoque e o valor unitário. Por exemplo: Feijão de Corda (pacote 1 Kg)</p>

```
  Nome: Feijão de Corda (pacote 1 Kg)
  Quantidade em estoque: 6
  Quantidade mínima para estoque: 4
  Valor unitário: R$ 5,00
```

<h3>Implemente um código em C# que tenha as funcionalidades descritas abaixo:</h3>
<p>  1 - Leia o nome do produto, sua quantidade em estoque, sua quantidade mínima para o estoque e o valor unitário. Devem ser lidas as informações de 10 (dez) produtos. Estas informações devem ser armazenadas em vetores separados, logo, 4 (quatro) vetores.</br>
  2 - Atualize a quantidade em estoque, quando um produto for vendido. A quantidade vendida deve ser armazenada em outro vetor. Quando um produto é vendido, o usuário informa a sua posição no vetor e quantidade vendida. Se a posição for inválida, deve ser impressa uma mensagem informativa para o usuário e ser lida uma nova posição Além disso, a quantidade em estoque é atualizada, ou seja, a quantidade em estoque no vetor que contém essa informação deve ser ajustada.</br>
  3 - Para cada venda efetuada, além das operações do item 2, o programa deve imprimir o nome do produto e o respectivo valor da venda.</br>
  4 - Gere um relatório contendo um balanço de todo o estoque e das vendas de cada produto: deve constar a descrição do produto, a quantidade em estoque, a quantidade mínima e se o produto está com o estoque normal ou abaixo do mínimo. Também deve ser informado o valor total de vendido até o momento de geração do relatório.</br>
  5 - O programa deve ser repetido até que o usuário não queira mais executá-lo.</br>
</p>
