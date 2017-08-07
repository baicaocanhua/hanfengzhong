http://requirejs.org/


一个是把它放在网页底部加载，另一个是写成下面这样：
async属性表明这个文件需要异步加载，避免网页失去响应。IE不支持这个属性，只支持defer，所以把defer也写上。
<script src="js/require.js" defer async="true" ></script>

asdad 