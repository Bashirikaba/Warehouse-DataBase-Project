<script setup lang="ts">
import { onMounted } from 'vue'
import { useInvoicesActions } from '../viewModels/InvoicesViewModel'
import StringFilterField from '@/components/StringFilterField.vue'
import { invoicesConfig } from '@/consts/tableConfigs'
import useFilterHelper from '../hooks/useFilterHelper'
import EntityTable from '@/components/EntityTable.vue'
import NumberFilterField from '@/components/NumberFilterField.vue'
import DateFilterField from '@/components/DateFilterField.vue'

const { invoices, filter, getAllInvoices, getInvoicesWithFilter } = useInvoicesActions()
const { getStringParam, getNumberParam, getDateParam } = useFilterHelper(filter)

onMounted(() => {
  getAllInvoices()
})
</script>

<template>
  <Card>
    <template #content>
      <div class="entity-title">
        <h1>Накладные</h1>
        <Button label="На домашнюю" as="router-link" :to="{ name: 'Home' }" />
      </div>
      <Divider></Divider>
      <div class="filters" style="display: flex; flex-direction: column; gap: 10px">
        <div
          class="filter"
          v-for="row in invoicesConfig.filter((r) => r.Field != 'Id')"
          :key="row.Field"
        >
          <StringFilterField
            v-if="row.Type == 'string'"
            :model-value="getStringParam(row.Field)"
            :label="row.Label"
          />
          <NumberFilterField
            v-if="row.Type == 'number'"
            :model-value="getNumberParam(row.Field)"
            :label="row.Label"
          />
          <DateFilterField
            v-if="row.Type == 'date'"
            :model-value="getDateParam(row.Field)"
            :label="row.Label"
          />
        </div>
      </div>
      <Button label="Поиск" style="margin-top: 1rem" @click="getInvoicesWithFilter()"></Button>
      <Divider></Divider>
      <EntityTable v-model="invoices" :config="invoicesConfig"></EntityTable>
    </template>
  </Card>
</template>
