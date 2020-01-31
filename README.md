<div class="flex xs12 md8" id="instructions"><h3 class="section-title"><i aria-hidden="true" class="v-icon material-icons theme--light">info_outline</i> Instruções
        </h3> <div><h1>Criptografia de Júlio César</h1>

<p>Segundo o Wikipedia, criptografia ou criptologia (em grego: kryptós, “escondido”, e gráphein, “escrita”) é o estudo e prática de princípios e técnicas para comunicação segura na presença de terceiros, chamados “adversários”. Mas geralmente, a criptografia refere-se à construção e análise de protocolos que impedem terceiros, ou o público, de lerem mensagens privadas. Muitos aspectos em segurança da informação, como confidencialidade, integridade de dados, autenticação e não-repúdio são centrais à criptografia moderna. Aplicações de criptografia incluem comércio eletrônico, cartões de pagamento baseados em chip, moedas digitais, senhas de computadores e comunicações militares. Das Criptografias mais curiosas na história da humanidade podemos citar a criptografia utilizada pelo grande líder militar romano Júlio César para comunicar com os seus generais. Essa criptografia se baseia na substituição da letra do alfabeto avançado um determinado número de casas. Por exemplo, considerando o número de casas <strong>= 3</strong>:</p>

<p><strong>Normal:</strong>  a ligeira raposa marrom saltou sobre o cachorro cansado</p>

<p><strong>Cifrado:</strong> d oljhlud udsrvd pduurp vdowrx vreuh r fdfkruur fdqvdgr</p>

<h2>Regras</h2>

<ul>
<li>As mensagens serão convertidas para minúsculas tanto para a criptografia quanto para descriptografia.</li>
<li>No nosso caso os números e pontos serão mantidos, ou seja:</li>
</ul>

<p><strong>Normal:</strong> 1a.a</p>

<p><strong>Cifrado:</strong> 1d.d</p>

<p>Escrever programa, em qualquer linguagem de programação, que faça uma requisição HTTP para a url abaixo:</p>

<pre><code>https://api.codenation.dev/v1/challenge/dev-ps/generate-data?token=SEU_TOKEN
</code></pre>

<p>Para encontrar o seu token , acesse a plataforma Codenation, faça o login e a informação estará na tela, conforme no exemplo abaixo:</p>

<p><img src="https://s3-us-west-1.amazonaws.com/codenation-cli/doc/images/token.png" width="728"></p>

<p>O resultado da requisição vai ser um JSON conforme o exemplo:</p>

<pre><code>{
	"numero_casas": 10,
	"token":"token_do_usuario",
	"cifrado": "texto criptografado",
	"decifrado": "aqui vai o texto decifrado",
	"resumo_criptografico": "aqui vai o resumo"
}
</code></pre>

<p>O primeiro passo é você salvar o conteúdo do JSON em um arquivo com o nome <strong>answer.json</strong>, que irá usar no restante do desafio.</p>

<p>Você deve usar o número de casas para decifrar o texto e atualizar o arquivo JSON, no campo <strong>decifrado</strong>. O próximo passo é gerar um resumo criptográfico do texto decifrado usando o algoritmo <strong>sha1</strong> e atualizar novamente o arquivo JSON. OBS: você pode usar qualquer biblioteca de criptografia da sua linguagem de programação favorita para gerar o resumo <strong>sha1</strong> do texto decifrado.</p>

<p>Seu programa deve submeter o arquivo atualizado para correção via POST para a API:</p>

<pre><code>https://api.codenation.dev/v1/challenge/dev-ps/submit-solution?token=SEU_TOKEN
</code></pre>

<p>OBS: a API espera um arquivo sendo enviado como <em>multipart/form-data</em>, como se fosse enviado por um formulário HTML, com um campo do tipo <em>file</em> com o nome <strong>answer</strong>. Considere isso ao enviar o arquivo.</p>

<p>O resultado da submissão vai ser sua nota ou o erro correspondente. Você pode submeter quantas vezes achar necessário, mas a API não vai permitir mais de uma submissão por minuto.</p>

<h2>OBS</h2>

<p>Neste estágio da aceleração não solicitamos que você nos envie o código do programa que você criou, mas recomendamos que você guarde uma cópia pois o mesmo pode ser solicitado nas próximas fases do processo.</p>
</div></div>
