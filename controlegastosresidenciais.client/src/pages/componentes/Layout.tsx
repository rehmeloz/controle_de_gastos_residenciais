import { Link } from "react-router-dom";

// Função responsável pelo layout do menu
export function Layout({ children }: { children: React.ReactNode }) {
    return (
        <>
            <nav className="menu">
                <Link to="/">Relatórios</Link>
                <Link to="/pessoas">Cadastro de Pessoas</Link>
                <Link to="/categorias">Cadastro de Categorias</Link>
                <Link to="/transacoes">Realizar Transações</Link>
            </nav>

            <div className="conteudo">
                {children}
            </div>
        </>
    );
}
