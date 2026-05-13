<script setup lang="ts">
import Api from '@/apiProvider/api'
import useFilterHelper from '@/hooks/useFilterHelper'
import type { IEntity, ISearchData, ITableConfigItem } from '@/types/interfaces'
import { onMounted, reactive, ref, type Reactive, type Ref } from 'vue'
import EnumFilterField from './EnumFilterField.vue'
import type { Service } from '@/types/types'
const { initFilter, getStringParam, getNumberParam, getDateParam, getValidatedSearchData } =
  useFilterHelper()

interface IReportTableProps {
  config: ITableConfigItem[]
  apiService: Service
  report: Report
}

const props = defineProps<IReportTableProps>()
const entities: Reactive<IEntity[]> = reactive([])
const isEntitiesLoading: Ref<boolean> = ref(true)
const filter: Reactive<ISearchData> = reactive(initFilter(props.config))

Api.setService(props.apiService)

onMounted(() => {
  buildReport()
})

async function buildReport() {
  isEntitiesLoading.value = true
  const response: IEntity[] = await Api.getReport<IEntity>(props.report)

  Object.assign(entities, response)
  isEntitiesLoading.value = false
}

async function buildFilteredReport() {
  isEntitiesLoading.value = true
  const response: IEntity[] = await Api.getReport<IEntity>(
    props.report,
    getValidatedSearchData(filter),
  )

  entities.length = 0
  Object.assign(entities, response)
  isEntitiesLoading.value = false
}
</script>
<template>
  <div @keydown.enter="buildFilteredReport()">
    <div class="fields">
      <div class="filter" v-for="row in config.filter((r) => r.Field != 'Id')" :key="row.Field">
        <StringFilterField
          v-if="row.Type == 'string'"
          :model-value="getStringParam(filter, row.Field)"
          :label="row.Label"
        />
        <NumberFilterField
          v-if="row.Type == 'number' && !row.Enum"
          :model-value="getNumberParam(filter, row.Field)"
          :label="row.Label"
        />
        <EnumFilterField
          v-if="row.Type == 'number' && row.Enum"
          :model-value="getNumberParam(filter, row.Field)"
          :label="row.Label"
        />
        <DateFilterField
          v-if="row.Type == 'date'"
          :model-value="getDateParam(filter, row.Field)"
          :label="row.Label"
        />
      </div>
    </div>
    <div style="display: flex; gap: 10px; margin-top: 1rem">
      <Button icon="pi pi-search" label="Поиск" @click="buildFilteredReport()"></Button>
    </div>
    <Divider></Divider>
    <DataTable
      :value="entities"
      striped-rows
      size="small"
      :sort-order="-1"
      show-gridlines
      data-key="Id"
      edit-mode="row"
      :loading="isEntitiesLoading"
    >
      <template #header>
        <div>Всего записей: {{ entities.length }}</div>
      </template>
      <template #empty>
        <div style="display: flex; flex-grow: 1; justify-content: center; padding: 20px">
          <span style="color: lightcoral">Данные не найдены</span>
        </div>
      </template>
      <Column
        v-for="row in config"
        :key="row.Field"
        sortable
        :field="row.Field"
        :header="row.Label"
        :data-type="row.Type"
        :hidden="row.Hidden"
      >
        <template v-if="row.Type == 'date'" #body="{ data }">
          {{ new Date(data[row.Field]).toLocaleDateString() }}
        </template>
        <template #editor="slotProps">
          <div v-if="row.Type == 'string'">
            <InputText fluid v-model="slotProps.data[row.Field]" />
          </div>
        </template>
      </Column>
    </DataTable>
  </div>
</template>
