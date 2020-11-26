import React from 'react';
import ProfessorCrud from "./ProfessorCrud";

class ProfessorLista extends React.Component {
    constructor(props) {
        super(props);
    }

    renderLista() {
        console.log(this.props);
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Matrícula</th>
                        <th>Nome</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {
                        this.props.objetos.map(obj =>
                            <tr key={obj.id}>
                                <td>{obj.matricula}</td>
                                <td>{obj.nome}</td>
                                <td>
                                    <button className="btn btn-primary mr-2" onClick={() => this.props.consultar(obj)}>Consultar</button>
                                    <button className="btn btn-warning mr-2" onClick={() => this.props.alterar(obj)}>Alterar</button>
                                    <button className="btn btn-danger mr-2" onClick={() => this.props.deletar(obj)}>Excluir</button>
                                </td>
                            </tr>
                        )
                    }
                </tbody>
            </table>
        );
    }

    render() {
        return (
            <div>
                <h1 id="tabelLabel" className="title" >Professores</h1>
                <div className="search-container"><input className="form-control" type="text" placeholder="Busca..." onChange={this.updateList} /></div>
                {this.renderLista()}
            </div>
        );
    }
    
    updateList = (event) => {
        console.log(event.target.value);
        this.props.buscar(event.target.value);
    }
}

export default ProfessorLista;