import React from 'react';
import ProfessorLista from './ProfessorLista';
import ProfessorManutencao from './ProfessorManutencao';
import axios from 'axios';
import ProfessorDeletar from './ProfessorDeletar';

class ProfessorCrud extends React.Component {
    constructor(props) {
        super(props);
        this.state = { objeto: null, objetos: [], status: CARREGANDO };
    }

    componentDidMount() {
        if (this.state.status === CARREGANDO) {
            this.listarApi();
        }
    }

    voltar = () => {
        this.setState({ status: LISTANDO });
    }

    salvar = (objeto) => {
        console.log("salvar:");
        console.log(objeto);
        if (this.state.status === INCLUINDO)
            this.incluirApi(objeto);
        else
            this.alterarApi(objeto);
    }

    listarApi = () => {
        axios.get('http://localhost:5000/api/professor')
        .then(result => this.setState({ objetos: result.data, status: LISTANDO }));
    };

    excluirApi = (id) => {
        axios.delete(`http://localhost:5000/api/professor/${id}`)
        .then(result => {
            console.log(result.status);
            this.listarApi();
        });
    };

    excluirObjeto = (id) => {
        this.excluirApi(id);
    }

    alterarApi = (objeto) => {
        axios.put(`http://localhost:5000/api/professor/${objeto.id}`,
            objeto
        )
        .then(result => {
            console.log(result.status);
            this.listarApi();
        });
    };

    incluirApi = (objeto) => {
        axios.post('http://localhost:5000/api/professor',
            objeto
        )
        .then(result => {
            console.log(result.status);
            this.listarApi();
        });
    };

    consultar = (objeto) => {
        this.setState({ objeto, status: CONSULTANDO });
    }

    incluir = () => {
        this.setState({ objeto: {}, status: INCLUINDO });
    }

    alterar = (objeto) => {
        this.setState({ objeto, status: ALTERANDO });
    }

    deletar = (objeto) => {
        this.setState({ objeto, status: DELETANDO });
    }

    renderCrud() {
        if (this.state.status === CARREGANDO)
            return <div>Carregando...</div>;
        else if (this.state.status === DELETANDO)
            return <ProfessorDeletar objeto={this.state.objeto} deletar={this.excluirApi} voltar={this.voltar} />
        else if (this.state.status === LISTANDO)
            return (
                <div>
                    <button className="btn btn-success mr-2" onClick={this.incluir}>Incluir</button>
                    <ProfessorLista objetos={this.state.objetos} alterar={this.alterar} deletar={this.deletar} consultar={this.consultar} />
                </div>
            );
        else
            return <ProfessorManutencao objeto={this.state.objeto} consultando={this.state.status === CONSULTANDO} salvar={this.salvar} voltar={this.voltar} />
    }

    render() {
        return (
            <div>
                {this.renderCrud()}
            </div>
        );
    }
}

const CARREGANDO = 0;
const LISTANDO = 1;
const CONSULTANDO = 2;
const INCLUINDO = 3;
const ALTERANDO = 4;
const DELETANDO = 5;

export default ProfessorCrud;
