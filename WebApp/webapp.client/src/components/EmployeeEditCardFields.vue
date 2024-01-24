<template>
    <v-col>
        <v-text-field v-model="editedItem.name"
                      label="Name"
                      append-icon="mdi-alert">
        </v-text-field>
    </v-col>
    <v-col>
        <v-text-field v-model="editedItem.surname"
                      label="Surname"
                      append-icon="mdi-alert"></v-text-field>
    </v-col>
    <v-col>
        <v-text-field v-model="editedItem.patronymic"
                      label="Patronymic"></v-text-field>
    </v-col>
    <v-col>
        <v-text-field v-model="editedItem.email"
                      label="Email"></v-text-field>
    </v-col>
    <v-col>
        <v-autocomplete v-model="editedItem.companyID"
                        :items="companies"
                        item-title="name"
                        item-value="id"
                        label="Company"
                        append-icon="mdi-alert">
        </v-autocomplete>
    </v-col>
</template>

<script>
    import apiClient from "../services/api";

    export default {
        props: {
            editedItem: Object,
        },
        data: () => ({
            companies: [],
        }),
        mounted() {
            this.loadCompanies()
        },
        methods: {
            async loadCompanies() {
                try {
                    const response = await apiClient.get('Companies');
                    this.companies = response.data;
                } catch (e) {
                    this.handleError(`Error loading data:${e}`);
                }
            },
        },
    }
</script>