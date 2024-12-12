import { useEffect, useState } from 'react';

function App() {
  const [produtos, setProdutos] = useState([]);
  const [nome, setNome] = useState('');
  const [preco, setPreco] = useState('');
  const [error, setError] = useState('');

  const fetchProdutos = async () => {
    try {
      const res = await fetch('http://localhost:5000/api/produtos');
      const data = await res.json();
      setProdutos(data);
    } catch (err) {
      setError('Erro ao buscar produtos.');
      console.log(err);
    }
  };

  const adicionarProduto = async () => {
    if (nome.trim() === '' || preco <= 0) {
      setError('Nome é obrigatório e Preço deve ser maior que zero.');
      return;
    }

    try {
      const res = await fetch('http://localhost:5000/api/produtos', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ nome, preco: parseFloat(preco) })
      });
      if (res.ok) {
        setNome('');
        setPreco('');
        fetchProdutos();
        setError('');
      } else {
        const errorData = await res.json();
        setError(errorData.message || 'Erro ao adicionar produto.');
      }
    } catch (err) {
      setError('Erro ao adicionar produto.');
      console.log(err);
    }
  };

  useEffect(() => {
    fetchProdutos();
  }, []);

  return (
    <div style={{ padding: "20px" }}>
      <h1>Produtos</h1>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      <ul>
        {produtos.map(p => (
          <li key={p.id}>{p.nome} - R${p.preco}</li>
        ))}
      </ul>
      <h2>Adicionar Produto</h2>
      <input
        placeholder="Nome"
        value={nome}
        onChange={e => setNome(e.target.value)}
      />
      <input
        placeholder="Preço"
        type="number"
        value={preco}
        onChange={e => setPreco(e.target.value)}
      />
      <button onClick={adicionarProduto}>Adicionar</button>
    </div>
  );
}

export default App;
