<script setup lang="ts">
import { stringSearchOperations } from '../consts/searchOperations'
import { useId } from 'vue'
import type { IStringParam } from '@/types/interfaces'

interface IStringFilterFieldProps {
  label: string
}

const uid: string = useId()
const param = defineModel<IStringParam>({ required: true })
defineProps<IStringFilterFieldProps>()
</script>

<template>
  <div class="filter-field">
    <FloatLabel variant="on">
      <InputText size="large" fluid :id="uid" v-model="param.Value" />
      <label :for="uid">{{ label }}</label>
    </FloatLabel>
    <Select
      v-model="param.Operation"
      :options="stringSearchOperations"
      option-value="Value"
      option-label="Name"
    />
  </div>
</template>
