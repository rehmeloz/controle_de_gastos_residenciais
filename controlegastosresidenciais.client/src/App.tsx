import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Pessoas } from "./pages/Pessoas/Pessoas";
import { Relatorios } from "./pages/Relatorios/Relatorios";
import { Layout } from "./pages/componentes/Layout";
import { Categorias } from "./pages/Categorias/Categorias";
import { Transacoes } from "./pages/Transacoes/Transacoes";

// Definindo rotas de navegação
function App() {
    return (
        <BrowserRouter>
            <Layout>
                <Routes>
                    <Route path="/" element={<Relatorios />} />
                    <Route path="/pessoas" element={<Pessoas />} />
                    <Route path="/categorias" element={<Categorias />} />
                    <Route path="/transacoes" element={<Transacoes />} />
                </Routes>
            </Layout>
        </BrowserRouter>
    );
}


export default App;
