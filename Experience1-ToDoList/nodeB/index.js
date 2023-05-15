const express =require('express')
const app= express();
const port =3300;
const sdk = require('api')('@render-api/v1.0#dnrc1ulf088q9j');

app.get("/",async (req, res) => {
    sdk.auth('rnd_ZQLtgdarvyGalU9uqAZbWYGAb96o');
    sdk.getServices({limit: '20'})
      .then(({ data }) =>  res.json(data))
      .catch(err => console.error(err));
  })
console.log("hi")
app.listen(port,()=>{
    console.log(`running http://localhost:${port}`)
})