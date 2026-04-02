import {
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Button,
    TextField,
    Stack,
    MenuItem
} from "@mui/material";
import { useCreateTransacao } from "../hooks/useCreateTransacao";
import { useToast } from "../../../app/feedbackProvider";
import { extractApiError } from "../../../api/interceptors";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm, Controller } from "react-hook-form";
import { createTransacaoSchema, type CreateTransacaoFormData, type CreateTransacaoFormInput } from "../schemas/createTransacaoSchema";
import { tipoTransacaoOptions } from "../../../domain/constants/tipoTransacaoOptions";
import { usePessoas } from "../../pessoas/hooks/usePessoas";
import { useCategorias } from "../../categorias/hooks/useCategorias";

interface Props {
    open: boolean;
    onClose: () => void;
}
export function CreateTransacaoDialog({ open, onClose }: Props) {
    const { mutateAsync, isPending } = useCreateTransacao();
    const { showToast } = useToast();
    const { data: pessoas } = usePessoas()
    const { data: categorias } = useCategorias();

    const {
        register,
        handleSubmit,
        control,
        reset,
        formState: { errors, isSubmitting },
    } = useForm<CreateTransacaoFormInput>({
        resolver: zodResolver(createTransacaoSchema),
        defaultValues: {
            descricao: "",
            valor: 0,
            tipo: undefined,
            data: "",
            pessoaId: "",
            categoriaId: ""
        }
    });

    async function onSubmit(data: CreateTransacaoFormInput) {
        try {
            await mutateAsync(data as CreateTransacaoFormData);
            showToast("Transação criada com sucesso", "success");
            reset({
                descricao: "",
                valor: 0,
                tipo: undefined,
                data: "",
                pessoaId: "",
                categoriaId: ""
            });
            onClose();
        } catch (err: any) {
            showToast(extractApiError(err), "error");
        }
    }

    function handleClose() {
        reset({
            descricao: "",
            valor: 0,
            tipo: undefined,
            data: "",
            pessoaId: "",
            categoriaId: ""
        });
        onClose();
    }

    return (
        <Dialog
            open={open}
            onClose={handleClose}
            fullWidth
            maxWidth="sm"
            keepMounted={false}
        >
            <DialogTitle>Nova Transação</DialogTitle>
            <form onSubmit={handleSubmit(onSubmit)}>
                <DialogContent>
                    <Stack spacing={2} mt={1}>
                        <TextField
                            label="Descrição"
                            {...register("descricao")}
                            error={!!errors.descricao}
                            helperText={errors.descricao?.message}
                        />
                        <TextField
                            type="number"
                            label="Valor"
                            {...register("valor", {
                                setValueAs: (value) => value === "" ? undefined : Number(value)
                            })}
                            error={!!errors.valor}
                            helperText={errors.valor?.message}
                        />
                        <Controller
                            name="tipo"
                            control={control}
                            render={({ field, fieldState }) => (
                                <TextField
                                    select
                                    label="Tipo"
                                    value={field.value ?? ""}
                                    onChange={(e) => field.onChange(Number(e.target.value))}
                                    error={!!fieldState.error}
                                    helperText={fieldState.error?.message}
                                >
                                    {tipoTransacaoOptions.map((opt) => (
                                        <MenuItem key={opt.value} value={opt.value}>
                                            {opt.label}
                                        </MenuItem>
                                    ))}
                                </TextField>
                            )}
                        />
                        <Controller
                            name="pessoaId"
                            control={control}
                            render={({ field, fieldState }) => (
                                <TextField
                                    select
                                    label="Pessoa"
                                    value={field.value ?? ""}
                                    onChange={(e) => field.onChange(e.target.value)}
                                    error={!!fieldState.error}
                                    helperText={fieldState.error?.message}
                                >
                                    <MenuItem disabled value="">
                                        {pessoas ? "Selecione..." : "Carregando..."}
                                    </MenuItem>
                                    {pessoas?.map((p) => (
                                        <MenuItem key={p.id} value={p.id}>
                                            {p.nome}
                                        </MenuItem>
                                    ))}
                                </TextField>
                            )}
                        />
                        <Controller
                            name="categoriaId"
                            control={control}
                            render={({ field, fieldState }) => (
                                <TextField
                                    select
                                    label="Categoria"
                                    value={field.value ?? ""}
                                    onChange={(e) => field.onChange(e.target.value)}
                                    error={!!fieldState.error}
                                    helperText={fieldState.error?.message}
                                >
                                    <MenuItem disabled value="">
                                        {pessoas ? "Selecione..." : "Carregando..."}
                                    </MenuItem>
                                    {categorias?.map((c) => (
                                        <MenuItem key={c.id} value={c.id}>
                                            {c.descricao}
                                        </MenuItem>
                                    ))}
                                </TextField>
                            )}
                        />
                        <TextField
                            type="date"
                            label="Data"
                            slotProps={{ inputLabel: { shrink: true } }}
                            {...register("data")}
                            error={!!errors.data}
                            helperText={errors.data?.message}
                        />
                    </Stack>
                </DialogContent>
                <DialogActions>
                    <Button
                        onClick={handleClose}
                        color="inherit"
                        variant="outlined"
                    >
                        Cancelar
                    </Button>

                    <Button
                        type="submit"
                        disabled={isSubmitting || isPending}
                    >
                        {isSubmitting || isPending ? "Salvando..." : "Salvar"}
                    </Button>
                </DialogActions>
            </form>
        </Dialog>
    );
}