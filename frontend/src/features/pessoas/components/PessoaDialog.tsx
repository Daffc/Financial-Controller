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
import { createPessoaSchema, type CreatePessoaFormData, type CreatePessoaFormInput } from "../schemas/pessoaSchema";
import { useUpdatePessoa } from "../hooks/useUpdatePessoa";
import { useEffect } from "react";
import type { UpdatePessoaRequest } from "../types/updatePessoasRequest";

interface Props {
    open: boolean;
    onClose: () => void;
    initialData?: UpdatePessoaRequest;
    mode?: "create" | "edit";
}
export function PessoaDialog({ open, onClose, initialData, mode }: Props) {
    const { mutateAsync: createPessoa, isPending: isCreating } = useCreatePessoa();
    const { mutateAsync: updatePessoa, isPending: isUpdating } = useUpdatePessoa();
    const { showToast } = useToast();

    const isEdit = mode === "edit";

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

    useEffect(() => {
        if (open) {
            reset(
                initialData
                    ? { nome: initialData.nome, idade: initialData.idade }
                    : { nome: "", idade: 0 }
            );
        }
    }, [open, initialData, reset]);


    async function onSubmit(data: CreatePessoaFormInput) {
        try {
            if (isEdit && initialData?.id) {
                await updatePessoa({
                    id: initialData.id,
                    nome: data.nome,
                    idade: data.idade!,
                });
                showToast("Pessoa atualizada com sucesso", "success");
            } else {
                await createPessoa(data as CreatePessoaFormData);
                showToast("Pessoa criada com sucesso", "success");
            }
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
            <DialogTitle>
                {isEdit ? "Ediar Pessoa" : "Nova Pessoa"}
            </DialogTitle>
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
                        disabled={isSubmitting || isCreating || isUpdating}
                    >
                        {
                            (isSubmitting || isCreating || isUpdating)
                                ? "Salvando..."
                                : isEdit ? "Atualizar" : "Salvar"
                        }
                    </Button>
                </DialogActions>
            </form>
        </Dialog>
    );
}