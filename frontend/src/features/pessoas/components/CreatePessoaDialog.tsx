import {
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Button,
    TextField,
    Stack
} from "@mui/material";
import { useCreatePessoa } from "../hooks/useCreatePessoa";
import { useToast } from "../../../app/feedbackProvider";
import { extractApiError } from "../../../api/interceptors";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { createPessoaSchema, type CreatePessoaFormData, type CreatePessoaFormInput } from "../schemas/createPessoaSchema";

interface Props {
    open: boolean;
    onClose: () => void;
}
export function CreatePessoaDialog({ open, onClose }: Props) {
    const { mutateAsync, isPending } = useCreatePessoa();
    const { showToast } = useToast();

    const {
        register,
        handleSubmit,
        reset,
        formState: { errors, isSubmitting },
    } = useForm<CreatePessoaFormInput>({
        resolver: zodResolver(createPessoaSchema),
        defaultValues: {
            nome: "",
            idade: 0
        }
    });

    async function onSubmit(data: CreatePessoaFormInput) {
        try {
            await mutateAsync(data as CreatePessoaFormData);
            showToast("Pessoa criada com sucesso", "success");
            reset({
                nome: "",
                idade: 0
            });
            onClose();
        } catch (err: any) {
            showToast(extractApiError(err), "error");
        }
    }

    function handleClose() {
        reset({
            nome: "",
            idade: 0
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
            <DialogTitle>Nova Pessoa</DialogTitle>
            <form onSubmit={handleSubmit(onSubmit)}>
                <DialogContent>
                    <Stack spacing={2} mt={1}>
                        <TextField
                            label="Nome"
                            {...register("nome")}
                            error={!!errors.nome}
                            helperText={errors.nome?.message}
                        />

                        <TextField
                            type="number"
                            label="Idade"
                            {...register("idade", {
                                setValueAs: (value) => value === "" ? undefined : Number(value)
                            })}
                            error={!!errors.idade}
                            helperText={errors.idade?.message}
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