import { TipoTransacao } from "../enums/TipoTransacao";
import { tipoTransacaoLabels } from "../mappers/tipoTransacaoMapper";

export const tipoTransacaoOptions = Object.values(TipoTransacao).map((value) => ({
    value,
    label: tipoTransacaoLabels[value],
}))